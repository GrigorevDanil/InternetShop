using CSharpFunctionalExtensions;
using InternetShop.Domain.Common;
using System.Text.RegularExpressions;

namespace InternetShop.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        private const string phoneRegex = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
        public const int MAX_PHONENUMBER_LENGHT = 13;

        public string Number { get; }

        private PhoneNumber(string number)
        {
            Number = number;
        }

        public static Result<PhoneNumber, Error> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Length > MAX_PHONENUMBER_LENGHT)
                return Errors.General.ValueIsRequired();

            if (Regex.IsMatch(input, phoneRegex) == false)
                return Errors.General.ValueIsInvalid();

            return new PhoneNumber(input);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
