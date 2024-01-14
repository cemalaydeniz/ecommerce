using ecommerce.Domain.Aggregates.OrderRepository.Entities;
using ecommerce.Domain.Aggregates.OrderRepository.Enums;
using ecommerce.Domain.Aggregates.OrderRepository.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderRepository
{
    public class Order : BaseEntity<Guid>, IAggregateRoot
    {
        #region Properties
        public string UserName { get; private set; }
        public Address DeliveryAddress { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public TicketStatus TicketStatus { get; private set; }

        // Navigations
        public Guid UserId { get; private set; }
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
