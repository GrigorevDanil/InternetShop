using CSharpFunctionalExtensions;
using InternetShop.Domain.Common;
using InternetShop.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace InternetShop.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        private readonly List<Order> _orders = [];

        private User()
        {

        }

        public User(FullName fullName, Gender gender)
        {
            FullName = fullName;
            Gender = gender;
        }

        public static Result<User, Error> Create(FullName fullName, Gender gender) =>
            new User(fullName, gender);

        public FullName FullName { get; } = default!;
        public Gender Gender { get; } = default!;
        public IReadOnlyList<Order> Orders => _orders;
    }
}
