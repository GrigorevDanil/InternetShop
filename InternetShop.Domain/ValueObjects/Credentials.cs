using CSharpFunctionalExtensions;
using InternetShop.Domain.Common;

namespace InternetShop.Domain.ValueObjects
{
    public class Credentials : ValueObject
    {
        public const int MAX_LOGIN_LENGHT = 20;
        public const int MAX_PASSWORD_HASH_LENGHT = 20;

        private Credentials(string login, string passwordHash)
        {
            Login = login;
            PasswordHash = passwordHash;
        }

        public string Login { get; }
        public string PasswordHash { get; }

        public static Result<Credentials, Error> Create(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || login.Length > MAX_LOGIN_LENGHT) return Errors.General.ValueIsRequired(nameof(login));

            if (string.IsNullOrEmpty(password) || password.Length > MAX_PASSWORD_HASH_LENGHT) return Errors.General.ValueIsRequired(nameof(password));

            return new Credentials(login, password);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Login;
            yield return PasswordHash;
        }
    }
}
