using CSharpFunctionalExtensions;
using InternetShop.Domain.Common;

namespace InternetShop.Domain.ValueObjects
{
    public class FullName : ValueObject
    {
        public const int MAX_SURNAME_LENGHT = 50;
        public const int MAX_NAME_LENGHT = 50;
        public const int MAX_LASTNAME_LENGHT = 50;

        private FullName(string surname, string name, string? lastname)
        {
            Surname = surname;
            Name = name;
            Lastname = lastname;
        }

        public static Result<FullName, Error> Create(string surname, string name, string? lastname)
        {
            if (string.IsNullOrEmpty(surname) || surname.Length > MAX_SURNAME_LENGHT) return Errors.General.ValueIsRequired(nameof(surname));

            if (string.IsNullOrEmpty(name) || name.Length > MAX_NAME_LENGHT) return Errors.General.ValueIsRequired(nameof(name));

            if (lastname?.Length > MAX_LASTNAME_LENGHT) return Errors.General.ValueIsRequired(nameof(lastname));

            return new FullName(surname, name, lastname);
        }

        public string Surname { get; }
        public string Name { get; }
        public string? Lastname { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Surname;
            yield return Name;
            if (Lastname != null) yield return Lastname;
        }
    }
}
