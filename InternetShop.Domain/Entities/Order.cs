using CSharpFunctionalExtensions;
using InternetShop.Domain.Common;
using InternetShop.Domain.ValueObjects;

namespace InternetShop.Domain.Entities
{
    public class Order : Entity<Guid>
    {
        private readonly List<OrderItem> _items = [];


        private Order()
        {

        }
        private Order(decimal totalAmount, OrderStatuses orderStatuses, PaymentStatuses paymentStatuses, DateTime? datePayment, User? user)
        {
            TotalAmount = totalAmount;
            OrderStatuses = orderStatuses;
            PaymentStatuses = paymentStatuses;
            DatePayment = datePayment;
            User = user;
        }

        public static Result<Order, Error> Create(decimal totalAmount, OrderStatuses orderStatuses, PaymentStatuses paymentStatuses, DateTime? datePayment, User? user)
        {
            return new Order(totalAmount, orderStatuses, paymentStatuses, datePayment, user);
        }

        public decimal TotalAmount { get; } = default!;
        public OrderStatuses OrderStatuses { get; } = default!;
        public PaymentStatuses PaymentStatuses { get; } = default!;
        public DateTime? DatePayment { get; }
        public User? User { get; }
        public IReadOnlyList<OrderItem> Items => _items;
    }
}
