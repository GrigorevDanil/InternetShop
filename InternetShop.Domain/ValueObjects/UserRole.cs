using CSharpFunctionalExtensions;
using InternetShop.Domain.Common;

namespace InternetShop.Domain.ValueObjects
{
    public class UserRole : ValueObject
    {
        public const int MAX_ROLE_LENGTH = 7;

        public static readonly UserRole Admin = new(nameof(Admin));
        public static readonly UserRole Manager = new(nameof(Manager));
        public static readonly UserRole User = new(nameof(User));

        private static readonly UserRole[] _all = [Admin, Manager, User];

        

        public string Value { get; }

        private UserRole(string value)
        {
            Value = value;
        }

        public static Result<UserRole, Error> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Errors.General.ValueIsRequired();

            var role = input.Trim().ToUpper();

            if (_all.Any(g => g.Value.ToUpper() == role) == false)
                return Errors.General.ValueIsInvalid();

            return new UserRole(role);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
