using CSharpFunctionalExtensions;
using InternetShop.Domain.Common;
using InternetShop.Domain.ValueObjects;

namespace InternetShop.Domain.Entities
{
    public class User : Entity<Guid>
    {
        private readonly List<Order> _orders = [];

        private User()
        {

        }

        private User(FullName fullName, Gender gender, PhoneNumber phoneNumber, Credentials credentials, UserRole role, Email email)
        {
            FullName = fullName;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Credentials = credentials;
            Role = role;
            Email = email;
        }

        public static Result<User, Error> Create(FullName fullName, Gender gender, PhoneNumber phoneNumber, Credentials credentials, UserRole role, Email email) =>
            new User(fullName, gender, phoneNumber, credentials, role, email);


        public FullName FullName { get; } = default!;
        public Gender Gender { get; } = default!;
        public PhoneNumber PhoneNumber { get; } = default!;
        public Credentials Credentials { get; } = default!;
        public UserRole Role { get; } = default!;
        public Email Email { get; } = default!;
        public IReadOnlyList<Order> Orders => _orders;
    }
}
