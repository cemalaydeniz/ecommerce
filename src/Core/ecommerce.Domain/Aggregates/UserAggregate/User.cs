using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.SeedWork;
using System.Text.RegularExpressions;

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

        // Navigations
        public List<Role> _roles = new List<Role>();
        public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
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

        #region Validations
        public static readonly string EmailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        public static readonly int EmailMaxLength = 100;
        public static readonly int NameMinLength = 3;
        public static readonly int NameMaxLength = 100;
        public static readonly int PhoneNumberMaxLength = 15;

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));
            if (email.Length > EmailMaxLength)
                throw new CharLengthOutofRangeException(nameof(email), EmailMaxLength);
            if (!Regex.IsMatch(email, EmailRegex))
                throw new InvalidEmailException("The email does not match with the regex expression");
        }

        private void ValidatePasswordHashed(string passwordHashed)
        {
            if (string.IsNullOrWhiteSpace(passwordHashed))
                throw new ArgumentNullException(nameof(passwordHashed));
        }

        private void ValidateName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            if (name.Length < NameMinLength || name.Length > NameMaxLength)
                throw new CharLengthOutofRangeException(nameof(name), NameMinLength, NameMaxLength);
        }

        private void ValidatePhoneNumber(string? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return;

            if (phoneNumber.Length > PhoneNumberMaxLength)
                throw new CharLengthOutofRangeException(nameof(phoneNumber), PhoneNumberMaxLength);
        }

        private void ValidateAddress(UserAddress address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));
        }
        #endregion
    }
}
