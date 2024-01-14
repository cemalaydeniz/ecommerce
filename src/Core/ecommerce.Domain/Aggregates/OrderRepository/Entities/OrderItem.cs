using ecommerce.Domain.Common.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderRepository.Entities
{
    public class OrderItem : BaseEntity<Guid>
    {
        #region Properties
        public string ProductName { get; private set; }
        public Money UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        // Navigations
        public Guid ProductId { get; private set; }
        #endregion
    }
}
