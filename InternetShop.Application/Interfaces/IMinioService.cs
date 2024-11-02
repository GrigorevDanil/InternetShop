using CSharpFunctionalExtensions;
using InternetShop.Domain.ValueObjects;

namespace InternetShop.Application.Interfaces
{
    public interface IMinioService
    {
        Task<Result> UploadImage(Stream stream, MainPhoto mainPhoto);
        Task<Stream> GetImage(string fileName);
    }
}
