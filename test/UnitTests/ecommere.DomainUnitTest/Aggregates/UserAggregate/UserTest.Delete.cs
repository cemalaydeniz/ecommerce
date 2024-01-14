using ecommerce.Domain.Aggregates.UserAggregate.Events;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommere.DomainUnitTest.Aggregates.UserAggregate.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.UserAggregate
{
    public partial class UserTest
    {
        public class DeleteTest
        {
            [Fact]
            public void DeleteUser_WhenUserIsAlreadyDeleted_ShouldReturnFalseAndNotAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                user.Delete();
                user.ClearDomainEvents();

                // Act
                var result = user.Delete();

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(UserSoftDeleted), user.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void DeleteUser_WhenUserIsNotDeleted_ShouldReturnTrueAndSetFlagAndClearEverythingAndAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;

                // Act
                var result = user.Delete();

                // Assert
                Assert.True(result);
                Assert.True(user.IsDeleted);
                Assert.Null(user.Email);
                Assert.Null(user.PasswordHashed);
                Assert.Null(user.Name);
                Assert.Null(user.PhoneNumber);
                Assert.Empty(user.Addresses);
                Assert.Contains(typeof(UserSoftDeleted), user.DomainEvents.Select(x => x.GetType()));
            }
        }
    }
}
