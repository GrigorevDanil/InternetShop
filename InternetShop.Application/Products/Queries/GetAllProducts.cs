using CSharpFunctionalExtensions;
using InternetShop.Application.Dtos;
using InternetShop.Application.Interfaces.Messaging;
using InternetShop.Domain.Interfaces.Repositories;

namespace InternetShop.Application.Products.Queries
{

    //public record GetFilteredProductsQuery(
    //    string Title,
    //    decimal MinPrice,
    //    decimal MaxPrice,
    //    string? SortBy,
    //    string? SortDirection) : IQuery;



    public class GetProductsHandler : IQueryHandler<IEnumerable<ProductDTO>>
    {
        private readonly IProductRepository productRepository;

        public GetProductsHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> Handle()
        {
            var products = await productRepository.Get();
            var productDtoList = products.Select(product => new ProductDTO(
                product.Id,
                product.Title,
                product.Description,
                product.Price,
                product.Count,
                product.MainPhoto.Path,
                product.Brand.Id,
                product.Category.Id
            ));

            return productDtoList;
        }
    }
}
