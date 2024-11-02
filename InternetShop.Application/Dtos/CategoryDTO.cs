
namespace InternetShop.Application.Dtos
{
    public record CategoryDTO(
        Guid Id,
        string Title,
        string Description,
        Guid ParentId);
}
