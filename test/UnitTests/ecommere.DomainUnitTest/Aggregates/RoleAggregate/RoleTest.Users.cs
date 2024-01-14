using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.RoleAggregate.Events;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommere.DomainUnitTest.Aggregates.RoleAggregate.Utilities;
using ecommere.DomainUnitTest.Aggregates.UserAggregate.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.RoleAggregate
{
    public partial class RoleTest
    {
        public class Users
        {
            #region Assign Role To User
            [Fact]
            public void AssignRoleToUser_WhenUserIsNull_ShouldThrowException()
            {
                // Arrange
                Role role = RoleTestUtility.ValidRole;

                // Act
                var result = Record.Exception(() =>
                {
                    role.AssignToUser(null!);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void AssignRoleToUser_WhenUserAlreadyHasIt_ShouldReturnFalseAndNotAddDomainEvent()
            {
                // Arrange
                Role role = RoleTestUtility.ValidRole;
                User user = UserTestUtility.ValidUser;
                role.AssignToUser(user);
                role.ClearDomainEvents();

                // Act
                var result = role.AssignToUser(user);

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(RoleAssignedToUser), role.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void AssignRoleToUser_WhenUserDoesNotHaveIt_ShouldReturnTrueAndAddToListAndAddDomainEvent()
            {
                // Arrange
                Role role = RoleTestUtility.ValidRole;
                User user = UserTestUtility.ValidUser;

                // Act
                var result = role.AssignToUser(user);

                // Assert
                Assert.True(result);
                Assert.Contains(typeof(RoleAssignedToUser), role.DomainEvents.Select(x => x.GetType()));
            }
            #endregion

            #region Remove Role From User
            [Fact]
            public void RemoveRoleFromUser_WhenUserIsNull_ShouldThrowException()
            {
                // Arrange
                Role role = RoleTestUtility.ValidRole;

                // Act
                var result = Record.Exception(() =>
                {
                    role.RemoveFromUser(null!);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void RemoveRoleFromUser_WhenUserDoesNotHaveIt_ShouldThrowException()
            {
                // Arrange
                Role role = RoleTestUtility.ValidRole;
                User user = UserTestUtility.ValidUser;

                // Act
                var result = Record.Exception(() =>
                {
                    role.RemoveFromUser(user);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void RemoveRoleFromUser_WhenRoleIsNotAssignedToUsers_ShouldThrowException()
            {
                // Arrange
                Role role = RoleTestUtility.ValidRole;
                User user = UserTestUtility.ValidUser;

                // Act
                var result = Record.Exception(() =>
                {
                    role.RemoveFromUser(user);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void RemoveRoleFromUser_WhenUserHasIt_ShouldReturnTrueAndRemoveFromListAndAddDomainEvent()
            {
                // Arrange
                Role role = RoleTestUtility.ValidRole;
                User user = UserTestUtility.ValidUser;
                role.AssignToUser(user);
                role.ClearDomainEvents();

                // Act
                var result = role.RemoveFromUser(user);

                // Assert
                Assert.True(result);
                Assert.DoesNotContain(user, role.Users);
                Assert.Contains(typeof(RoleRemovedFromUser), role.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void RemoveFromAllUsers_WhenRoleIsNotAssignedToUsers_ShouldReturnFalseAndNotAddDomainEvent()
            {
                // Arrange
                Role role = RoleTestUtility.ValidRole;

                // Act
                var result = role.RemoveFromAllUsers();

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(RoleRemovedFromUsers), role.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void RemoveFromAllUsers_WhenRoleIsAssignedToSomeUsers_ShouldReturnTrueAndClearListAndAddDomainEvent()
            {
                // Arrange
                Role role = RoleTestUtility.ValidRole;
                User user = UserTestUtility.ValidUser;
                role.AssignToUser(user);
                role.ClearDomainEvents();

                // Act
                var result = role.RemoveFromAllUsers();

                // Assert
                Assert.True(result);
                Assert.Empty(role.Users);
                Assert.Contains(typeof(RoleRemovedFromUsers), role.DomainEvents.Select(x => x.GetType()));
            }
            #endregion
        }
    }
}
