using CSharpFunctionalExtensions;
using InternetShop.Domain.Common;

namespace InternetShop.Domain.Entities
{
    public class Product : Entity<Guid>
    {
        public const int MAX_TITLE_LENGHT = 50;
        public const int MAX_DESCRIPTION_LENGHT = 150;

        private readonly List<OrderItem> _items = [];

        private Product()
        {

        }
        private Product(string title, string description, decimal price, int count, Brand? brand, Category? category)
        {
            Title = title;
            Description = description;
            Price = price;
            Count = count;
            Brand = brand;
            Category = category;
        }

        public static Result<Product, Error> Create(string title, string description, decimal price, int count, Brand? brand, Category? category)
        {
            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGHT) Errors.General.ValueIsRequired(nameof(title));
            if (string.IsNullOrEmpty(description) || description.Length > MAX_DESCRIPTION_LENGHT) Errors.General.ValueIsRequired(nameof(description));

            return new Product(title, description, price, count, brand, category);
        }

        public string Title { get; } = default!;
        public string Description { get; } = default!;
        public decimal Price { get; }
        public int Count { get; }
        public Brand? Brand { get; }
        public Category? Category { get; }
        public IReadOnlyList<OrderItem> Items => _items;
    }
}
