#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.Events;
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
        private User() { }

        /// <summary>
        /// Creates a new user locally
        /// </summary>
        /// <param name="email">The email address of the user</param>
        /// <param name="passwordHashed">The hashed password of the user</param>
        /// <param name="name">The name of the user</param>
        /// <param name="phoneNumber">The phone number of the user</param>
        /// <param name="initialAddress">The initial address of the user</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        /// <exception cref="InvalidEmailException"></exception>
        public User(string email,
            string passwordHashed,
            string? name,
            string? phoneNumber,
            UserAddress? initialAddress)
        {
            name = string.IsNullOrWhiteSpace(name) ? null : name;
            phoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : phoneNumber;

            ValidateEmail(email);
            ValidatePasswordHashed(passwordHashed);
            ValidateName(name);
            ValidatePhoneNumber(phoneNumber);

            Id = Guid.NewGuid();
            Email = email;
            PasswordHashed = passwordHashed;
            Name = name;
            PhoneNumber = phoneNumber;

            Addresses = new List<UserAddress>();
            if (initialAddress != null)
            {
                Addresses.Add(initialAddress);
            }

            base.AddDomainEvents(new UserCreated(this));
        }

        /// <summary>
        /// Updates the email address of the user
        /// </summary>
        /// <param name="newEmail">The new email address to be set</param>
        /// <returns>TRUE if the new email address is set successfully, FALSE if the new email address is already the same as the old one</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        /// <exception cref="InvalidEmailException"></exception>
        public bool UpdateEmail(string newEmail)
        {
            ValidateEmail(newEmail);

            if (Email == newEmail)
                return false;

            Email = newEmail;

            base.AddDomainEvents(new UserEmailUpdated(this, newEmail));
            return true;
        }

        /// <summary>
        /// Updates the password of the user
        /// </summary>
        /// <param name="newPasswordHashed">The new hashed password to be set</param>
        /// <returns>TRUE if the new hashed password is set successfully, FALSE if the new hashed password is already the same as the old one</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool UpdatePasswordHashed(string newPasswordHashed)
        {
            ValidatePasswordHashed(newPasswordHashed);

            if (PasswordHashed == newPasswordHashed)
                return false;

            PasswordHashed = newPasswordHashed;

            base.AddDomainEvents(new UserPasswordUpdated(this));
            return true;
        }

        /// <summary>
        /// Updates the name of the user
        /// </summary>
        /// <param name="newName">The new name to be set</param>
        /// <returns>TRUE if the new name is set successfully, FALSE if the new name is already the same as the old one</returns>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        public bool UpdateName(string? newName)
        {
            newName = string.IsNullOrWhiteSpace(newName) ? null : newName;
            ValidateName(newName);

            if (Name == newName)
                return false;

            Name = newName;

            base.AddDomainEvents(new UserNameUpdated(this, newName));
            return true;
        }

        /// <summary>
        /// Updates the phone number of the user
        /// </summary>
        /// <param name="newPhoneNumber">The new phone number to be set</param>
        /// <returns>TRUE if the new phone number is set successfully, FALSE if the new phone number is already the same as the old one</returns>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        public bool UpdatePhoneNumber(string? newPhoneNumber)
        {
            newPhoneNumber = string.IsNullOrWhiteSpace(newPhoneNumber) ? null : newPhoneNumber;
            ValidatePhoneNumber(newPhoneNumber);

            if (PhoneNumber == newPhoneNumber)
                return false;

            PhoneNumber = newPhoneNumber;

            base.AddDomainEvents(new UserPhoneNumberUpdated(this, newPhoneNumber));
            return true;
        }

        /// <summary>
        /// Adds a new address to the address list of the user
        /// </summary>
        /// <param name="newAddress">The new address to be added to the list</param>
        /// <returns>TRUE if the new address is added successfully, FALSE if the new address already exists in the address list</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool AddAddress(UserAddress newAddress)
        {
            ValidateAddress(newAddress);

            if (Addresses.Contains(newAddress))
                return false;

            Addresses.Add(newAddress);

            base.AddDomainEvents(new UserAddressAdded(this, newAddress));
            return true;
        }

        /// <summary>
        /// Updates one of the addresses of the user
        /// </summary>
        /// <param name="titleofAddressToUpdate">The title of the address to be updated</param>
        /// <param name="newAddress">The new address to be set</param>
        /// <returns>TRUE if the address is updated successfully, FALSE if there address is the same as the old one</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        /// <exception cref="AddressNotFoundException"></exception>
        /// <exception cref="DuplicatedAddressTitleException"></exception>
        public bool UpdateAddress(string titleofAddressToUpdate, UserAddress newAddress)
        {
            ValidateAddress(newAddress);

            UserAddress? address = Addresses.Find(a => a.Title == titleofAddressToUpdate);
            if (address == null)
                throw new AddressNotFoundException($"No address with the title of {titleofAddressToUpdate} was found");

            if (titleofAddressToUpdate == newAddress.Title)
            {
                if (address.Address == newAddress.Address)
                    return false;
            }
            else
            {
                if (Addresses.Contains(newAddress))
                    throw new DuplicatedAddressTitleException($"{newAddress.Title} already exists in the list");
            }

            int index = Addresses.IndexOf(address);
            Addresses[index] = new UserAddress(newAddress.Title, newAddress.Address);

            base.AddDomainEvents(new UserAddressUpdated(this, titleofAddressToUpdate, newAddress));
            return true;
        }

        /// <summary>
        /// Removes an address from the address list of the user
        /// </summary>
        /// <param name="addressToRemove">The address to be removed from the list</param>
        /// <returns>TRUE if the address is removed successfully, FALSE if the address does not exist in the address list</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool RemoveAddress(UserAddress addressToRemove)
        {
            ValidateAddress(addressToRemove);

            UserAddress? address = Addresses.Find(a => a == addressToRemove);
            if (address == null)
                return false;

            Addresses.Remove(address);

            base.AddDomainEvents(new UserAddressRemoved(this, address));
            return true;
        }

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

            base.AddDomainEvents(new UserSoftDeleted(Id));
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
