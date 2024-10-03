using CSharpFunctionalExtensions;
using InternetShop.Domain.Common;

namespace InternetShop.Domain.ValueObjects
{
    public class OrderStatuses : ValueObject
    {
        public const int MAX_STATUS_LENGHT = 10;

        public static readonly OrderStatuses Processing = new(nameof(Processing));
        public static readonly OrderStatuses Shipped = new(nameof(Shipped));
        public static readonly OrderStatuses Delivered = new(nameof(Delivered));
        public static readonly OrderStatuses Finished = new(nameof(Finished));

        private static readonly OrderStatuses[] _all = [Processing, Shipped, Delivered, Finished];

        public string Value { get;}

        private OrderStatuses(string value)
        {
            Value = value;
        }

        public static Result<OrderStatuses, Error> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Errors.General.ValueIsRequired();

            var status = input.Trim().ToUpper();

            if (_all.Any(g => g.Value.ToUpper() == status) == false)
                return Errors.General.ValueIsInvalid();

            return new OrderStatuses(status);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
