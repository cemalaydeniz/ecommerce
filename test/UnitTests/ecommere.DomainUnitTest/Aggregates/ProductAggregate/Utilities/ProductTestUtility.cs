using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.TestUtility;
using ecommere.DomainUnitTest.Common.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.ProductAggregate.Utilities
{
    public static class ProductTestUtility
    {
        public static string ValidName => "Name";
        public static List<Money> ValidPrices => new List<Money>() { MoneyTestUtility.ValidMoney,       // Currency codes are: XYZ and ABC
            new Money("ABC", MoneyTestUtility.ValidAmount) };
        public static List<Money> ValidFreePrices => new List<Money>() { Money.Zero };                  // Currency code is: USD
        public static string ValidDescription => "Description";

        public static Product ValidProduct => new Product(ValidName,
            ValidPrices,
            ValidDescription);
        public static Product ValidFreeProduct => new Product(ValidName,
            ValidFreePrices,
            ValidDescription);

        #region Valid Values
        public static IEnumerable<object?[]> ValidNames()
        {
            yield return new object?[] { ValidName };
        }

        public static IEnumerable<object?[]> ValidPriceLists()
        {
            yield return new object?[] { ValidPrices };
            yield return new object?[] { ValidFreePrices };
        }

        public static IEnumerable<object?[]> ValidDescriptions()
        {
            yield return new object?[] { ValidDescription };
        }

        public static IEnumerable<object?[]> ValidValues()
        {
            foreach (var values in ValueGenerator(ValidNames,
                ValidPriceLists,
                ValidDescriptions))
            {
                yield return values;
            }
        }
        #endregion

        #region Invalid Values
        public static IEnumerable<object?[]> InvalidNames()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(Product.NameMinLength - 1) };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(Product.NameMaxLength + 1) };
        }

        public static IEnumerable<object?[]> InvalidPriceLists()
        {
            yield return new object?[] { null };
            yield return new object?[] { new List<Money>() };
            yield return new object?[] { new List<Money>() { MoneyTestUtility.ValidMoney, MoneyTestUtility.ValidMoney } };
            yield return new object?[] { new List<Money>() { MoneyTestUtility.ValidMoney, Money.Zero } };
        }

        public static IEnumerable<object?[]> InvalidDescriptions()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(Product.DescriptionMinLength - 1) };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(Product.DescriptionMaxLength + 1) };
        }

        public static IEnumerable<object?[]> InvalidValues()
        {
            foreach (var values in ValueGenerator(InvalidNames,
                InvalidPriceLists,
                InvalidDescriptions))
            {
                yield return values;
            }
        }
        #endregion

        private static IEnumerable<object?[]> ValueGenerator(Func<IEnumerable<object?[]>> names,
            Func<IEnumerable<object?[]>> prices,
            Func<IEnumerable<object?[]>> descriptions)
        {
            foreach (var name in names())
            {
                yield return new object?[]
                {
                    name[0],
                    ValidPrices,
                    ValidDescription
                };
            }
            foreach (var price in prices())
            {
                yield return new object?[]
                {
                    ValidName,
                    price[0],
                    ValidDescription
                };
            }
            foreach (var description in descriptions())
            {
                yield return new object?[]
                {
                    ValidName,
                    ValidPrices,
                    description[0]
                };
            }
        }
    }
}
