using ecommerce.Domain.Aggregates.OrderRepository.Entities;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.TestUtility;
using ecommerce.DomainUnitTest.Common.Utilities;

namespace ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities
{
    public static class OrderItemTestUtility
    {
        public static string ValidProductName => "Product name";
        public static int ValidQuantity => 2;

        public static OrderItem ValidOrderItem => new OrderItem(Guid.NewGuid(),
            ValidProductName,
            MoneyTestUtility.ValidMoney,
            ValidQuantity);

        #region Valid Values
        public static IEnumerable<object?[]> ValidProductNames()
        {
            yield return new object?[] { ValidProductName };
        }

        public static IEnumerable<object?[]> ValidQuantities()
        {
            yield return new object?[] { ValidQuantity };
        }

        public static IEnumerable<object?[]> ValidValues()
        {
            foreach (var values in ValueGenerator(ValidProductNames, ValidQuantities))
            {
                yield return values;
            }
        }
        #endregion

        #region Invalid Values
        public static IEnumerable<object?[]> InvalidProductNames()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(Product.NameMinLength - 1) };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(Product.NameMaxLength + 1) };
        }

        public static IEnumerable<object?[]> InvalidQuantities()
        {
            yield return new object?[] { 0 };
            yield return new object?[] { -1 };
        }

        public static IEnumerable<object?[]> InvalidValues()
        {
            foreach (var values in ValueGenerator(InvalidProductNames, InvalidQuantities))
            {
                yield return values;
            }
        }
        #endregion

        private static IEnumerable<object?[]> ValueGenerator(Func<IEnumerable<object?[]>> productNames,
            Func<IEnumerable<object?[]>> quantities)
        {
            foreach (var productName in productNames())
            {
                yield return new object?[]
                {
                    Guid.NewGuid(),
                    productName[0],
                    MoneyTestUtility.ValidMoney,
                    ValidQuantity
                };
            }
            foreach (var quantity in quantities())
            {
                yield return new object?[]
                {
                    Guid.NewGuid(),
                    ValidProductName,
                    MoneyTestUtility.ValidMoney,
                    quantity[0]
                };
            }
        }
    }
}
