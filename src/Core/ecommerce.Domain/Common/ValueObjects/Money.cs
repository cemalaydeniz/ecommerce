#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Common.ValueObjects
{
    public sealed class Money : ValueObject
    {
        public string CurrencyCode { get; private set; }
        public decimal Amount { get; private set; }

        private Money() { }

        /// <summary>
        /// Creates new money information
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        /// <exception cref="NegativeMoneyException"></exception>
        public Money(string currencyCode, decimal amount)
        {
            ValidateCurrencyCode(currencyCode);
            ValiateAmount(amount);

            CurrencyCode = currencyCode;
            Amount = amount;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return CurrencyCode;
            yield return Amount;
        }

        #region Validations
        public static readonly int CurrenyCodeMaxLength = 3;
        public static readonly int AmountMaxDigitBeforeComma = 18;
        public static readonly int AmountMaxDigitAfterComma = 2;

        private void ValidateCurrencyCode(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                throw new ArgumentNullException(nameof(currencyCode));
            if (currencyCode.Length > CurrenyCodeMaxLength)
                throw new CharLengthOutofRangeException(nameof(currencyCode), CurrenyCodeMaxLength);
        }

        private void ValiateAmount(decimal amount)
        {
            if (amount < 0)
                throw new NegativeMoneyException($"The given amount is {amount}");

            decimal integerPart = Math.Floor(amount);
            if (integerPart.ToString().Length > AmountMaxDigitBeforeComma)
                throw new MoneyExceedException($"The amount can have a maximum of {AmountMaxDigitBeforeComma} digits before the comma");

            decimal decimalPart = amount - integerPart;
            if (decimalPart.ToString().Length - 2 > AmountMaxDigitAfterComma)
                throw new MoneyExceedException($"The amount can have a maximum of {AmountMaxDigitAfterComma} digits after the comma");
        }
        #endregion

        public static Money Zero => new Money("USD", 0);
    }
}
