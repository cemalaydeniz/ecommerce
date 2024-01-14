using ecommerce.Domain.Aggregates.OrderRepository.Enums;
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
    }
}
