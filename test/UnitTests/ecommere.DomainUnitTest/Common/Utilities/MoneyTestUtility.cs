using ecommerce.Domain.Common.ValueObjects;
using ecommerce.TestUtility;

namespace ecommere.DomainUnitTest.Common.Utilities
{
    public static class MoneyTestUtility
    {
        public static string ValidCurrencyCode => "XYZ";
        public static decimal ValidAmount => 10.99m;

        public static Money ValidMoney => new Money(ValidCurrencyCode, ValidAmount);

        #region Valid Values
        public static IEnumerable<object?[]> ValidCurrencyCodes()
        {
            yield return new object?[] { ValidCurrencyCode };
        }

        public static IEnumerable<object?[]> ValidAmounts()
        {
            yield return new object?[] { ValidAmount };
        }

        public static IEnumerable<object?[]> ValidValues()
        {
            foreach (var values in ValueGenerator(ValidCurrencyCodes, ValidAmounts))
            {
                yield return values;
            }
        }
        #endregion

        #region Invalid Values
        public static IEnumerable<object?[]> InvalidCurrencyCodes()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(Money.CurrenyCodeMaxLength + 1) };
        }

        public static IEnumerable<object?[]> InvalidAmounts()
        {
            yield return new object[] { -1 };
            yield return new object[] { 1234567890123456789.12m };
            yield return new object[] { 123456789012345678.123m };
            yield return new object[] { 1234567890123456789.123m };
        }

        public static IEnumerable<object?[]> InvalidValues()
        {
            foreach (var values in ValueGenerator(InvalidCurrencyCodes, InvalidAmounts))
            {
                yield return values;
            }
        }
        #endregion

        private static IEnumerable<object?[]> ValueGenerator(Func<IEnumerable<object?[]>> currencyCodes, Func<IEnumerable<object?[]>> amounts)
        {
            foreach (var currencyCode in currencyCodes())
            {
                yield return new object?[]
                {
                    currencyCode[0],
                    ValidAmount
                };
            }
            foreach (var amount in amounts())
            {
                yield return new object?[]
                {
                    ValidCurrencyCode,
                    amount[0]
                };
            }
        }
    }
}
