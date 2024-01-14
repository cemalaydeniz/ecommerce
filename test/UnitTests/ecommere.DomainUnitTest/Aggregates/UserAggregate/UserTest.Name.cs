using ecommerce.Domain.Aggregates.UserAggregate.Events;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;

namespace ecommerce.DomainUnitTest.Aggregates.UserAggregate
{
    public partial class UserTest
    {
        public class NameTest
        {
            [Theory]
            [MemberData(nameof(UserTestUtility.InvalidNames), MemberType = typeof(UserTestUtility))]
            public void UpdateName_WhenNameIsInvalid_ShouldThrowException(string name)
            {
                // Arrange
                User user = UserTestUtility.ValidUser;

                // Act
                var result = Record.Exception(() =>
                {
                    user.UpdateName(name);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void UpdateName_WhenNameIsSame_ShouldReturnFalseAndNotAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                var name = user.Name;

                // Act
                var result = user.UpdateName(name);

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(UserNameUpdated), user.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void UpdateName_WhenNameIsDifferent_ShouldReturnTrueAndChangeNameAndAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                var name = user.Name + "a";

                // Act
                var result = user.UpdateName(name);

                // Assert
                Assert.True(result);
                Assert.Equal(name, user.Name);
                Assert.Contains(typeof(UserNameUpdated), user.DomainEvents.Select(x => x.GetType()));
            }
        }
    }
}
