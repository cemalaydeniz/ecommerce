using ecommerce.Domain.Aggregates.OrderAggregate.Entities;
using ecommerce.Domain.Aggregates.OrderAggregate.Events;
using ecommerce.Domain.Aggregates.OrderAggregate;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities;

namespace ecommerce.DomainUnitTest.Aggregates.OrderAggregate
{
    public partial class OrderTest
    {
        [Theory]
        [MemberData(nameof(OrderTestUtility.ValidValues), MemberType = typeof(OrderTestUtility))]
        public void CreateOrderEntity_WhenValuesAreValid_ShouldNotThrowExceptionAndAddDomainEvent(Guid userId,
            string userName,
            Address deliveryAddress,
            List<OrderItem> orderItems)
        {
            // Arrange
            Order? order = null;

            // Act
            var result = Record.Exception(() =>
            {
                order = new Order(userId, userName, deliveryAddress, orderItems);
            });

            // Assert
            Assert.Null(result);
            Assert.Contains(typeof(OrderCreated), order!.DomainEvents.Select(x => x.GetType()));
        }

        [Theory]
        [MemberData(nameof(OrderTestUtility.InvalidValues), MemberType = typeof(OrderTestUtility))]
        public void CreateOrderEntity_WhenValuesAreValid_ShouldNotThrowException(Guid userId,
            string userName,
            Address deliveryAddress,
            List<OrderItem> orderItems)
        {
            // Act
            var result = Record.Exception(() =>
            {
                new Order(userId, userName, deliveryAddress, orderItems);
            });

            // Assert
            Assert.NotNull(result);
        }
    }
}
