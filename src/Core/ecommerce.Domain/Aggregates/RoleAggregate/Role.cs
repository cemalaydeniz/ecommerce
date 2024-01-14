#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.Aggregates.RoleAggregate.Events;
using ecommerce.Domain.Aggregates.RoleAggregate.Exceptions;
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

        #region Behaviors
        private Role() { }

        /// <summary>
        /// Creates a new role locally
        /// </summary>
        /// <param name="name">The name of the role</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        public Role(string name)
        {
            ValidateName(name);

            Id = Guid.NewGuid();
            Name = name;

            base.AddDomainEvents(new RoleCreated(this));
        }

        /// <summary>
        /// Updates the name of the role
        /// </summary>
        /// <param name="newName">The new name to be set</param>
        /// <returns>TRUE if the new name is set successfully, FALSE if the new name is already the same as the old one</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        public bool UpdateName(string newName)
        {
            ValidateName(newName);

            if (Name == newName)
                return false;

            Name = newName;

            base.AddDomainEvents(new RoleNameUpdated(this, newName));
            return true;
        }

        /// <summary>
        /// Assigns the role to a user
        /// </summary>
        /// <param name="user">The user to be assigned the role</param>
        /// <returns>TRUE if the role is assigned to the user successfully, FALSE if the user already has the role</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool AssignToUser(User user)
        {
            ValidateUser(user);

            if (_users.Contains(user))
                return false;

            _users.Add(user);

            base.AddDomainEvents(new RoleAssignedToUser(this, user));
            return true;
        }

        /// <summary>
        /// Removes the role from a user
        /// </summary>
        /// <param name="user">The user to remove the role from</param>
        /// <returns>TRUE if the role is removed from the user successfully, FALSE if there is no user in this role</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UserNotFoundInRoleException"></exception>
        public bool RemoveFromUser(User user)
        {
            ValidateUser(user);

            User? userToRemoveRoleFrom = _users.Find(u => u == user);
            if (userToRemoveRoleFrom == null)
                throw new UserNotFoundInRoleException();

            if (_users.Count == 0)
                return false;

            _users.Remove(userToRemoveRoleFrom);

            base.AddDomainEvents(new RoleRemovedFromUser(this, user));
            return true;
        }

        /// <summary>
        /// Removes the role from the all users
        /// </summary>
        /// <returns>TRUE if the role is removed from the users successfully, FALSE if the role is already not assigned to a single user</returns>
        public bool RemoveFromAllUsers()
        {
            if (_users.Count == 0)
                return false;

            _users.Clear();

            base.AddDomainEvents(new RoleRemovedFromUsers(this));
            return true;
        }
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
