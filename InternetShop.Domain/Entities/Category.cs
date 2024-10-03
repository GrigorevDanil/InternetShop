using CSharpFunctionalExtensions;
using InternetShop.Domain.Common;

namespace InternetShop.Domain.Entities
{
    public class Category : Entity<Guid>
    {
        public const int MAX_TITLE_LENGHT = 30;
        public const int MAX_DESCRIPTION_LENGHT = 100;

        private readonly List<Category> _categories = [];
        private readonly List<Product> _products = [];


        private Category()
        {

        }
        private Category(string title, string? description, Category? parent)
        {
            Title = title;
            Description = description;
            Parent = parent;
        }

        public static Result<Category, Error> Create(string title, string? description, Category? parent)
        {
            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGHT) Errors.General.ValueIsRequired(nameof(title));

            if (description?.Length > MAX_DESCRIPTION_LENGHT) Errors.General.ValueIsRequired(nameof(description));

            return new Category(title, description, parent);
        }

        public string Title { get; } = default!;
        public string? Description { get; }
        public Category? Parent { get; }
        public IReadOnlyList<Category> Categories => _categories;
        public IReadOnlyList<Product> Products => _products;

    }
}
