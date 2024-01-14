using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.UserAggregate
{
    public class User : BaseEntity<Guid>, IAggregateRoot, ISoftDelete
    {
        #region Properties
        public string? Email { get; private set; }
        public string? PasswordHashed { get; private set; }
        public string? Name { get; private set; }
        public string? PhoneNumber { get; private set; }
        public List<UserAddress> Addresses { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        #endregion

        #region Behaviors
        /// <summary>
        /// Marks the user as deleted and removes all personal information about the user to comply with GDPR
        /// </summary>
        /// <returns>TRUE if the user is marked as deleted successfully and removed all information, FALSE if the user is already marked as deleted</returns>
        public bool Delete()
        {
            if (IsDeleted)
                return false;

            Email = null;
            PasswordHashed = null;
            Name = null;
            PhoneNumber = null;
            Addresses.Clear();
            IsDeleted = true;

            return true;
        }
        #endregion
    }
}
