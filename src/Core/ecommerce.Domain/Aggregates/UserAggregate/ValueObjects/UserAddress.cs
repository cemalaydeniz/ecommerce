#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.UserAggregate.ValueObjects
{
    public sealed class UserAddress : ValueObject
    {
        public string Title { get; private set; }
        public Address Address { get; private set; }

        private UserAddress() { }

        /// <summary>
        /// Creates a new user address information
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        public UserAddress(string title, Address address)
        {
            ValidateTitle(title);
            ValidateAddress(address);

            Title = title;
            Address = address;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Title;

            /**
             * In this example, the address information is not included in the process of comparison. Only the titles of the addresses
             * are checked if they are the same. If the address information needs to be included in comparison, then depending on the requirements,
             * the title of the address might becomes an independent parameter for the comparison in order to not allow duplicated titles.
             * In this case, the add/update/remove logics for the user addresses need to be changed as well, or more than one address that has the same title
             * can be allowed, as long as both the title and the address are not the same. This depends on the business logic.
             */
            //foreach (var item in Address.GetEqualityComponent())
            //{
            //    yield return item;
            //}
        }

        #region Validations
        public static readonly int TitleMaxLength = 50;

        private void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));
            if (title?.Length > TitleMaxLength)
                throw new CharLengthOutofRangeException(nameof(title), TitleMaxLength);
        }

        private void ValidateAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));
        }
        #endregion
    }
}
