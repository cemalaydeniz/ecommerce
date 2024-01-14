using ecommerce.Domain.Common.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.ProductAggregate
{
    public class Product : BaseEntity<Guid>, ISoftDelete
    {
        #region Properties
        public string Name { get; private set; }
        public List<Money> Prices { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        #endregion

        #region Behaviors
        /// <summary>
        /// Marks the product as deleted
        /// </summary>
        /// <returns>TRUE if the product is marked as deleted successfully, FALSE if the product is already marked as deleted</returns>
        public bool Delete()
        {
            if (IsDeleted)
                return false;

            IsDeleted = true;

            return true;
        }
        #endregion
    }
}
