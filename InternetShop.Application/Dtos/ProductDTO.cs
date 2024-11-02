
namespace InternetShop.Application.Dtos
{
    public record ProductDTO(
        Guid Id,
        string Title,
        string Description,
        decimal Price,
        int Count,
        string MainPhoto,
        Guid BrandId,
        Guid CategoryId);
}
