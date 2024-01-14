using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.RoleAggregate.Events;
using ecommere.DomainUnitTest.Aggregates.RoleAggregate.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.RoleAggregate
{
    public partial class RoleTest
    {
        public class NameTest
        {
            [Theory]
            [MemberData(nameof(RoleTestUtility.InvalidNames), MemberType = typeof(RoleTestUtility))]
            public void UpdateName_WhenNameIsInvalid_ShouldThrowException(string name)
            {
                // Arrange
                Role role = RoleTestUtility.ValidRole;

                // Act
                var result = Record.Exception(() =>
                {
                    role.UpdateName(name);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void UpdateName_WhenNameIsSame_ShouldReturnFalseAndNotAddDomainEvent()
            {
                // Arrange
                Role role = RoleTestUtility.ValidRole;
                var name = role.Name;

                // Act
                var result = role.UpdateName(name);

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(RoleNameUpdated), role.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void UpdateName_WhenNameIsDifferent_ShouldReturnTrueAndChangeNameAndAddDomainEvent()
            {
                // Arrange
                Role role = RoleTestUtility.ValidRole;
                var name = role.Name + "a";

                // Act
                var result = role.UpdateName(name);

                // Assert
                Assert.True(result);
                Assert.Equal(name, role.Name);
                Assert.Contains(typeof(RoleNameUpdated), role.DomainEvents.Select(x => x.GetType()));
            }
        }
    }
}
