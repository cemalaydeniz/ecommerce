namespace ecommerce.Application.Utilities.Constants
{
    public static partial class ConstantsUtility
    {
        public static class MoneyValidation
        {
            public static readonly string MoneyRequired = "Money is required";
            public static readonly string CurrencyCodeRequired = "Currency code is required";
            public static readonly string AmounRequired = "Amount is required";

            public static readonly string CurrencyCodeLength_Max = $"Currency code cannot be longer than {Domain.Common.ValueObjects.Money.CurrenyCodeMaxLength} characters";
            public static readonly string AmountCannotBeNegative = "Amount cannot be negative";
            public static readonly string AmountMaxDigitBeforeComma = $"The amount can have a maximum of {Domain.Common.ValueObjects.Money.AmountMaxDigitBeforeComma} digits before the comma";
            public static readonly string AmountMaxDigitAfterComma = $"The amount can have a maximum of {Domain.Common.ValueObjects.Money.AmountMaxDigitAfterComma} digits after the comma";
        }
    }
}
