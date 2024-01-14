using ecommerce.Domain.Aggregates.OrderRepository.Enums;
using ecommerce.Domain.Aggregates.OrderRepository.Events;
using ecommerce.Domain.Aggregates.OrderRepository;
using ecommere.DomainUnitTest.Aggregates.OrderAggregate.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.OrderAggregate
{
    public partial class OrderTest
    {
        public class TicketTest
        {
            #region Add Message To Ticket
            [Fact]
            public void AddMessageToTicket_WhenMessageIsNull_ShouldThrowError()
            {
                // Arrange
                Order order = OrderTestUtility.ValidOrder;

                // Act
                var result = Record.Exception(() =>
                {
                    order.AddMessageToTicket(null!);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void AddMessageToTicket_WhenTicketIsNotOpened_ShouldReturnTrueAndChangeStatusAndAddMessageAndAddDomainEvent()
            {
                // Arrange
                Order order = OrderTestUtility.ValidOrder;
                var message = TicketMessageTestUtility.ValidTicketMessage;

                // Act
                var result = order.AddMessageToTicket(message);

                // Assert
                Assert.True(result);
                Assert.Equal(TicketStatus.Opened, order.TicketStatus);
                Assert.Contains(message, order.TicketMessages);
                Assert.Contains(typeof(OrderTicketMessageAdded), order.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void AddMessageToTicket_WhenTicketIsOpened_ShouldReturnTrueAndAddMessageAndAddDomainEvent()
            {
                // Arrange
                Order order = OrderTestUtility.ValidOrder;
                order.AddMessageToTicket(TicketMessageTestUtility.ValidTicketMessage);
                order.ClearDomainEvents();
                var message = TicketMessageTestUtility.ValidTicketMessage;

                // Act
                var result = order.AddMessageToTicket(message);

                // Assert
                Assert.True(result);
                Assert.Equal(TicketStatus.Opened, order.TicketStatus);
                Assert.Contains(message, order.TicketMessages);
                Assert.Contains(typeof(OrderTicketMessageAdded), order.DomainEvents.Select(x => x.GetType()));
            }
            #endregion

            #region Close Ticket
            [Fact]
            public void CloseTicket_WhenTicketIsNotOpened_ShouldReturnFalseAndDoesNotAddDomainEvent()
            {
                // Arrange
                Order order = OrderTestUtility.ValidOrder;

                // Act
                var result = order.CloseTicket();

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(OrderTicketClosed), order.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void CloseTicket_WhenTicketIsOpened_ShouldReturnTrueAndChangeStatusAndAddDomainEvent()
            {
                // Arrange
                Order order = OrderTestUtility.ValidOrder;
                order.AddMessageToTicket(TicketMessageTestUtility.ValidTicketMessage);
                order.ClearDomainEvents();

                // Act
                var result = order.CloseTicket();

                // Assert
                Assert.True(result);
                Assert.Equal(TicketStatus.Closed, order.TicketStatus);
                Assert.Contains(typeof(OrderTicketClosed), order.DomainEvents.Select(x => x.GetType()));
            }
            #endregion
        }
    }
}
