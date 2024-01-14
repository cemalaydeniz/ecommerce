using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderRepository.Entities
{
    public class TicketMessage : BaseEntity<Guid>
    {
        #region Properties
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Navigations
        public Guid UserId { get; private set; }
        #endregion
    }
}
