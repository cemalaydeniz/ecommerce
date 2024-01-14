using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Common.Exceptions;
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

        #region Validations
        public static readonly int NameMinLength = 3;
        public static readonly int NameMaxLength = 20;

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (name.Length < NameMinLength || name.Length > NameMaxLength)
                throw new CharLengthOutofRangeException(nameof(name), NameMinLength, NameMaxLength);
        }

        private void ValidateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
        }
        #endregion
    }
}
