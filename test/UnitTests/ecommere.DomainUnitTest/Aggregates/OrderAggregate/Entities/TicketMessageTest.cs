using ecommerce.Domain.Aggregates.OrderAggregate.Entities;
using ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities;

namespace ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Entities
{
    public class TicketMessageTest
    {
        [Theory]
        [MemberData(nameof(TicketMessageTestUtility.ValidValues), MemberType = typeof(TicketMessageTestUtility))]
        public void CreateTicketMessageEntity_WhenValuesAreValid_ShouldNotThrowException(Guid userId,
            string content)
        {
            // Arrange
            TicketMessage? ticketMessage = null;

            // Act
            var result = Record.Exception(() =>
            {
                ticketMessage = new TicketMessage(userId, content);
            });

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(TicketMessageTestUtility.InvalidValues), MemberType = typeof(TicketMessageTestUtility))]
        public void CreateTicketMessageEntity_WhenValuesAreValid_ShouldThrowException(Guid userId,
            string content)
        {
            // Act
            var result = Record.Exception(() =>
            {
                new TicketMessage(userId, content);
            });

            // Assert
            Assert.NotNull(result);
        }
    }
}
