using ecommerce.Domain.Aggregates.UserAggregate.Events;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommere.DomainUnitTest.Aggregates.UserAggregate.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.UserAggregate
{
    public partial class UserTest
    {
        public class EmailTest
        {
            [Theory]
            [MemberData(nameof(UserTestUtility.InvalidEmails), MemberType = typeof(UserTestUtility))]
            public void UpdateEmail_WhenEmailIsValid_ShouldThrowException(string email)
            {
                // Arrange
                User user = UserTestUtility.ValidUser;

                // Act
                var result = Record.Exception(() =>
                {
                    user.UpdateEmail(email);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void UpdateEmail_WhenEmailIsSame_ShouldReturnFalseAndNotAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                var email = user.Email;

                // Act
                var result = user.UpdateEmail(email!);

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(UserEmailUpdated), user.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void UpdateEmail_WhenEmailIsDifferent_ShouldReturnTrueAndChangeEmailAndAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                var email = user.Email + "a";

                // Act
                var result = user.UpdateEmail(email);

                // Assert
                Assert.True(result);
                Assert.Equal(email, user.Email);
                Assert.Contains(typeof(UserEmailUpdated), user.DomainEvents.Select(x => x.GetType()));
            }
        }
    }
}
