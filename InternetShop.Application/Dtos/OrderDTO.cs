
namespace InternetShop.Application.Dtos
{
    public record OrderDTO(
        Guid Id,
        decimal TotalAmount,
        string OrderStatus,
        string PaymentStatus,
        DateTime DatePayment,
        Guid UserId, 
        OrderItemDTO[] Items);
}
