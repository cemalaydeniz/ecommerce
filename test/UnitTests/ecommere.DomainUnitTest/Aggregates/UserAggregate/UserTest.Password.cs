using ecommerce.Domain.Aggregates.UserAggregate.Events;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;

namespace ecommerce.DomainUnitTest.Aggregates.UserAggregate
{
    public partial class UserTest
    {
        public class PasswordTest
        {
            [Theory]
            [MemberData(nameof(UserTestUtility.InvalidPasswordHasheds), MemberType = typeof(UserTestUtility))]
            public void UpdatePasswordHashed_WhenHashedPasswordIsInvalid_ShouldThrowException(string passwordHashed)
            {
                // Arrange
                User user = UserTestUtility.ValidUser;

                // Act
                var result = Record.Exception(() =>
                {
                    user.UpdatePasswordHashed(passwordHashed);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void UpdatePasswordHashed_WhenHashedPasswordIsSame_ShouldReturnFalseAndNotAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                var passwordHashed = user.PasswordHashed;

                // Act
                var result = user.UpdatePasswordHashed(passwordHashed!);

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(UserPasswordUpdated), user.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void UpdatePasswordHashed_WhenHashedPasswordIsDifferent_ShouldReturnTrueAndChangePasswordAndAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                var passwordHashed = user.PasswordHashed + "a";

                // Act
                var result = user.UpdatePasswordHashed(passwordHashed);

                // Assert
                Assert.True(result);
                Assert.Equal(passwordHashed, user.PasswordHashed);
                Assert.Contains(typeof(UserPasswordUpdated), user.DomainEvents.Select(x => x.GetType()));
            }
        }
    }
}
