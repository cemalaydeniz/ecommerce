using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.RoleAggregate.Events;
using ecommere.DomainUnitTest.Aggregates.RoleAggregate.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.RoleAggregate
{
    public partial class RoleTest
    {
        [Theory]
        [MemberData(nameof(RoleTestUtility.ValidValues), MemberType = typeof(RoleTestUtility))]
        public void CreateRoleEntity_WhenValuesAreValid_ShouldThrowNoExceptionAndAddDomainEvent(string name)
        {
            // Arrange
            Role? role = null;

            // Act
            var result = Record.Exception(() =>
            {
                role = new Role(name);
            });

            // Assert
            Assert.Null(result);
            Assert.Contains(typeof(RoleCreated), role!.DomainEvents.Select(x => x.GetType()));
        }

        [Theory]
        [MemberData(nameof(RoleTestUtility.InvalidValues), MemberType = typeof(RoleTestUtility))]
        public void CreateRoleEntity_WhenValuesAreInvalid_ShouldThrowException(string name)
        {
            // Act
            var result = Record.Exception(() =>
            {
                new Role(name);
            });

            // Assert
            Assert.NotNull(result);
        }
    }
}
