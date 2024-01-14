using ecommerce.Domain.Aggregates.UserAggregate.Events;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommere.DomainUnitTest.Aggregates.UserAggregate.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.UserAggregate
{
    public partial class UserTest
    {
        [Theory]
        [MemberData(nameof(UserTestUtility.ValidValues), MemberType = typeof(UserTestUtility))]
        public void CreateUserEntity_WhenValuesAreValid_ShouldThrowNoExceptionAndAddDomainEvent(string email,
            string passwordHashed,
            string? name,
            string? phoneNumber,
            UserAddress? initialAddress)
        {
            // Arrange
            User? user = null;

            // Act
            var result = Record.Exception(() =>
            {
                user = new User(email, passwordHashed, name, phoneNumber, initialAddress);
            });

            // Assert
            Assert.Null(result);
            Assert.Contains(typeof(UserCreated), user!.DomainEvents.Select(x => x.GetType()));
        }

        [Theory]
        [MemberData(nameof(UserTestUtility.InvalidValues), MemberType = typeof(UserTestUtility))]
        public void CreateUserEntity_WhenValuesAreInvalid_ShouldThrowException(string email,
            string passwordHashed,
            string? name,
            string? phoneNumber,
            UserAddress? initialAddress)
        {
            // Act
            var result = Record.Exception(() =>
            {
                new User(email, passwordHashed, name, phoneNumber, initialAddress);
            });

            // Assert
            Assert.NotNull(result);
        }
    }
}
