using ecommerce.Domain.Aggregates.UserAggregate.Events;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommere.DomainUnitTest.Aggregates.UserAggregate.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.UserAggregate
{
    public partial class UserTest
    {
        public class PhoneNumberTest
        {
            [Theory]
            [MemberData(nameof(UserTestUtility.InvalidPhoneNumbers), MemberType = typeof(UserTestUtility))]
            public void UpdatePhoneNumber_WhenPhoneNumberIsInvalid_ShouldThrowException(string phoneNumber)
            {
                // Arrange
                User user = UserTestUtility.ValidUser;

                // Act
                var result = Record.Exception(() =>
                {
                    user.UpdatePhoneNumber(phoneNumber);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void UpdatePhoneNumber_WhenPhoneNumberIsSame_ShouldReturnFalseAndNotAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                var phoneNumber = user.PhoneNumber;

                // Act
                var result = user.UpdatePhoneNumber(phoneNumber);

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(UserPhoneNumberUpdated), user.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void UpdatePhoneNumber_WhenPhoneNumberIsDifferent_ShouldReturnTrueAndChangePhoneNumberAndAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                var phoneNumber = user.PhoneNumber + "a";

                // Act
                var result = user.UpdatePhoneNumber(phoneNumber);

                // Assert
                Assert.True(result);
                Assert.Equal(phoneNumber, user.PhoneNumber);
                Assert.Contains(typeof(UserPhoneNumberUpdated), user.DomainEvents.Select(x => x.GetType()));
            }
        }
    }
}
