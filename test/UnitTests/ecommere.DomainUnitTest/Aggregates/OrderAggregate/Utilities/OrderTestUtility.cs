using ecommerce.Domain.Aggregates.OrderRepository.Entities;
using ecommerce.Domain.Aggregates.OrderRepository;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.TestUtility;
using ecommerce.DomainUnitTest.Common.Utilities;

namespace ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities
{
    public static class OrderTestUtility
    {
        public static string ValidUserName => "User name";
        public static List<OrderItem> ValidOrderItems => new List<OrderItem>() { OrderItemTestUtility.ValidOrderItem };

        public static Order ValidOrder => new Order(Guid.NewGuid(),
            ValidUserName,
            AddressTestUtility.ValidAddress,
            ValidOrderItems);

        #region Valid Values
        public static IEnumerable<object?[]> ValidUserNames()
        {
            yield return new object?[] { ValidUserName };
        }

        public static IEnumerable<object?[]> ValidAddresses()
        {
            yield return new object?[] { AddressTestUtility.ValidAddress };
        }

        public static IEnumerable<object?[]> ValidOrderItemLists()
        {
            yield return new object?[] { ValidOrderItems };
        }

        public static IEnumerable<object?[]> ValidValues()
        {
            foreach (var values in ValueGenerator(ValidUserNames,
                ValidAddresses,
                ValidOrderItemLists))
            {
                yield return values;
            }
        }
        #endregion

        #region Invalid Values
        public static IEnumerable<object?[]> InvalidUserNames()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(User.NameMinLength - 1) };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(User.NameMaxLength + 1) };
        }

        public static IEnumerable<object?[]> InvalidAddresses()
        {
            yield return new object?[] { null };
        }

        public static IEnumerable<object?[]> InvalidOrderItemLists()
        {
            yield return new object?[] { null };
            yield return new object?[] { new List<OrderItem>() };
            Guid sameId = Guid.NewGuid();
            yield return new object?[] { new List<OrderItem>()
                {
                    new OrderItem(sameId, OrderItemTestUtility.ValidProductName, MoneyTestUtility.ValidMoney, OrderItemTestUtility.ValidQuantity),
                    new OrderItem(sameId, OrderItemTestUtility.ValidProductName, MoneyTestUtility.ValidMoney, OrderItemTestUtility.ValidQuantity)
                } };
        }

        public static IEnumerable<object?[]> InvalidValues()
        {
            foreach (var values in ValueGenerator(InvalidUserNames,
                InvalidAddresses,
                InvalidOrderItemLists))
            {
                yield return values;
            }
        }
        #endregion

        private static IEnumerable<object?[]> ValueGenerator(Func<IEnumerable<object?[]>> userNames,
            Func<IEnumerable<object?[]>> addresses,
            Func<IEnumerable<object?[]>> orderItemLists)
        {
            foreach (var userName in userNames())
            {
                yield return new object?[]
                {
                    Guid.NewGuid(),
                    userName[0],
                    AddressTestUtility.ValidAddress,
                    ValidOrderItems
                };
            }
            foreach (var address in addresses())
            {
                yield return new object?[]
                {
                    Guid.NewGuid(),
                    ValidUserName,
                    address[0],
                    ValidOrderItems
                };
            }
            foreach (var orderItemsList in orderItemLists())
            {
                yield return new object?[]
                {
                    Guid.NewGuid(),
                    ValidUserName,
                    AddressTestUtility.ValidAddress,
                    orderItemsList[0]
                };
            }
        }
    }
}
