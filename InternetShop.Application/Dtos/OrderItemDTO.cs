
namespace InternetShop.Application.Dtos
{
    public record OrderItemDTO(
        Guid Id,
        decimal Price,
        int Count,
        Guid ProductId,
        Guid OrderId);
}
