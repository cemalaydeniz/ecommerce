using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.RoleAggregate
{
    public class Role : BaseEntity<Guid>, IAggregateRoot
    {
        #region Properties
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }

        // Navigations
        private List<User> _users = new List<User>();
        public IReadOnlyCollection<User> Users => _users.AsReadOnly();
        #endregion
    }
}
