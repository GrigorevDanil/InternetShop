using CSharpFunctionalExtensions;
using InternetShop.Application.Interfaces;
using InternetShop.Application.Interfaces.Messaging;
using InternetShop.Domain.Common;
using InternetShop.Domain.Entities;
using InternetShop.Domain.Interfaces.Repositories;
using InternetShop.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace InternetShop.Application.Products.Commands
{
    public record PublishProductCommand(
        string Title,
        string Description,
        decimal Price,
        int Count,
        IFormFile MainPhoto,
        Guid BrandId,
        Guid CategoryId,
        IEnumerable<Guid> OrderItems) : ICommand;

    public class PublishProductHandler : ICommandHandler<Guid, PublishProductCommand>
    {
        private readonly IProductRepository productRepository;
        private readonly IBrandRepository brandRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMinioService minioService;

        public PublishProductHandler(IProductRepository productRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository, IMinioService minioService)
        {
            this.productRepository = productRepository;
            this.brandRepository = brandRepository;
            this.categoryRepository = categoryRepository;
            this.minioService = minioService;
        }

        public async Task<Result<Guid, ErrorList>> Handle(PublishProductCommand command)
        {
            using var stream = command.MainPhoto.OpenReadStream();
            var contentType = command.MainPhoto.ContentType;
            var extension = Path.GetExtension(command.MainPhoto.FileName);
            var mainPhoto = MainPhoto.Create(extension, contentType).Value;

            var uploadResult = await minioService.UploadImage(stream, mainPhoto);

            var brand = await brandRepository.GetById(command.BrandId);

            var category = await categoryRepository.GetById(command.CategoryId);

            var product = Product.Create(
                command.Title,
                command.Description,
                command.Price,
                command.Count,
                mainPhoto,
                brand,
                category);

            await productRepository.Add(product.Value);

            return product.Value.Id;
        }
    }
}
