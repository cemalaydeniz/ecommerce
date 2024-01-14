using ecommerce.Domain.Aggregates.OrderRepository.Entities;
using ecommerce.Domain.Common.ValueObjects;
using ecommere.DomainUnitTest.Aggregates.OrderAggregate.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.OrderAggregate.Entities
{
    public class OrderItemTest
    {
        [Theory]
        [MemberData(nameof(OrderItemTestUtility.ValidValues), MemberType = typeof(OrderItemTestUtility))]
        public void CreateOrderItemEntity_WhenValuesAreValid_ShouldNotThrowException(Guid productId,
            string productName,
            Money unitPrice,
            int quantity)
        {
            // Arrange
            OrderItem? orderItem = null;

            // Act
            var result = Record.Exception(() =>
            {
                orderItem = new OrderItem(productId, productName, unitPrice, quantity);
            });

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(OrderItemTestUtility.InvalidValues), MemberType = typeof(OrderItemTestUtility))]
        public void CreateOrderItemEntity_WhenValuesAreValid_ShouldThrowException(Guid productId,
            string productName,
            Money unitPrice,
            int quantity)
        {
            // Act
            var result = Record.Exception(() =>
            {
                new OrderItem(productId, productName, unitPrice, quantity);
            });

            // Assert
            Assert.NotNull(result);
        }
    }
}
