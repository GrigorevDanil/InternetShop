using CSharpFunctionalExtensions;
using InternetShop.Domain.Common;

namespace InternetShop.Domain.Entities
{
    public class OrderItem : Entity<Guid>
    {
        private OrderItem()
        {

        }
        private OrderItem(decimal price, int count, Product? product, Order? order)
        {
            Price = price;
            Count = count;
            Product = product;
            Order = order;
        }

        public static Result<OrderItem, Error> Create(decimal price, int count, Product? product, Order? order)
        {
            return new OrderItem(price, count, product, order);
        }


        public decimal Price { get; }
        public int Count { get; }
        public Product? Product { get; }
        public Order? Order { get; }
    }
}
