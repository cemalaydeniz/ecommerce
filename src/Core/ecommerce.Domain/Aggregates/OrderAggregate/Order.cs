#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.Aggregates.OrderAggregate.Entities;
using ecommerce.Domain.Aggregates.OrderAggregate.Enums;
using ecommerce.Domain.Aggregates.OrderAggregate.Events;
using ecommerce.Domain.Aggregates.OrderAggregate.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderAggregate
{
    public class Order : BaseEntity<Guid>, IAggregateRoot
    {
        #region Properties
        public string UserName { get; private set; }
        public Address DeliveryAddress { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public TicketStatus TicketStatus { get; private set; }

        // Navigations
        private List<OrderItem> _orderItems = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        private List<TicketMessage> _ticketMessages = new List<TicketMessage>();
        public IReadOnlyCollection<TicketMessage> TicketMessages => _ticketMessages.AsReadOnly();

        public Guid UserId { get; private set; }
        #endregion

        #region Behaviors
        private Order() { }

        /// <summary>
        /// Creates a new order locally
        /// </summary>
        /// <param name="userId">The Id of the owner</param>
        /// <param name="userName">The name of the owner</param>
        /// <param name="deliveryAddress">The address of the owner</param>
        /// <param name="issuedAt">The time when the process was initially issued</param>
        /// <param name="orderItems">The items of the order</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="DuplicatedOrderItemException"></exception>
        public Order(Guid userId,
            string userName,
            Address deliveryAddress,
            List<OrderItem> orderItems)
        {
            ValidateUserName(userName);
            ValidateAddress(deliveryAddress);
            ValidateOrderItems(orderItems);

            Id = Guid.NewGuid();
            UserName = userName;
            DeliveryAddress = deliveryAddress;
            TicketStatus = TicketStatus.NotOpened;

            _orderItems = orderItems;
            UserId = userId;

            base.AddDomainEvents(new OrderCreated(this));
        }

        /// <summary>
        /// Adds a message to the customer support ticket of the order. If the ticket is not opened,
        /// it also changes the status of the ticket
        /// </summary>
        /// <param name="ticketMessage">The ticket message to be added</param>
        /// <returns>TRUE if message is added to the ticket, FALSE if the ticket is already closed</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool AddMessageToTicket(TicketMessage ticketMessage)
        {
            ValidateTicketMessage(ticketMessage);

            if (TicketStatus == TicketStatus.Closed)
                return false;

            TicketStatus = TicketStatus.Opened;
            _ticketMessages.Add(ticketMessage);

            base.AddDomainEvents(new OrderTicketMessageAdded(this, ticketMessage));
            return true;
        }

        /// <summary>
        /// Closes the customer support ticket of the order
        /// </summary>
        /// <returns>TRUE if the ticket is closed successfully, FALSE if the ticket was already closed</returns>
        public bool CloseTicket()
        {
            if (TicketStatus != TicketStatus.Opened)
                return false;

            TicketStatus = TicketStatus.Closed;

            base.AddDomainEvents(new OrderTicketClosed(this));
            return true;
        }
        #endregion

        #region Validations
        private void ValidateUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));
            if (userName.Length < User.NameMinLength || userName.Length > User.NameMaxLength)
                throw new CharLengthOutofRangeException(nameof(userName), User.NameMinLength, User.NameMaxLength);
        }

        private void ValidateAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));
        }

        private void ValidateOrderItems(List<OrderItem> orderItems)
        {
            if (orderItems == null)
                throw new ArgumentNullException(nameof(orderItems));
            if (orderItems.Count == 0)
                throw new InvalidOperationException($"{nameof(orderItems)} is empty");
            if (orderItems.GroupBy(oi => oi.ProductId).Any(x => x.Count() > 1))
                throw new DuplicatedOrderItemException("There is one or more duplicated products in the list. They must be grouped together");
        }

        private void ValidateTicketMessage(TicketMessage ticketMessage)
        {
            if (ticketMessage == null)
                throw new ArgumentNullException(nameof(ticketMessage));
        }
        #endregion
    }
}
