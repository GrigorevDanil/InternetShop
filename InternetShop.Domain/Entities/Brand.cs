using CSharpFunctionalExtensions;
using InternetShop.Domain.Common;

namespace InternetShop.Domain.Entities
{
    public class Brand : Entity<Guid>
    {
        public const int MAX_TITLE_LENGHT = 30;

        private readonly List<Product> _products = [];

        private Brand()
        {

        }

        private Brand(string title)
        {
            Title = title;
        }

        public static Result<Brand, Error> Create(string title)
        {
            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGHT) Errors.General.ValueIsRequired(nameof(title));

            return new Brand(title);
        }

        public string Title { get; } = default!;
        public IReadOnlyList<Product> Products => _products;
    }
}
