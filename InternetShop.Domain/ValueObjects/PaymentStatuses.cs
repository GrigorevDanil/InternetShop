using CSharpFunctionalExtensions;
using InternetShop.Domain.Common;

namespace InternetShop.Domain.ValueObjects
{
    public class PaymentStatuses : ValueObject
    {
        public const int MAX_STATUS_LENGHT = 7;

        public static readonly PaymentStatuses Paid = new(nameof(Paid));
        public static readonly PaymentStatuses NotPaid = new(nameof(NotPaid));

        private static readonly PaymentStatuses[] _all = [Paid, NotPaid];

        public string Value { get; }

        public static Result<PaymentStatuses, Error> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Errors.General.ValueIsRequired();

            var status = input.Trim().ToUpper();

            if (_all.Any(g => g.Value.ToUpper() == status) == false)
                return Errors.General.ValueIsInvalid();

            return new PaymentStatuses(status);
        }

        private PaymentStatuses(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
