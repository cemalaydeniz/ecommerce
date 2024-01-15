using ecommerce.Application.Features.Commands.RemoveRoleFromAllUsers;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.RoleAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.RemoveRoleFromAllUsers
{
    public class RemoveRoleFromAllUsersCommandHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private RemoveRoleFromAllUsersCommandHandler _removeRoleFromAllUsersCommandHandler;

        public RemoveRoleFromAllUsersCommandHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _removeRoleFromAllUsersCommandHandler = new RemoveRoleFromAllUsersCommandHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task RemoveRoleFromAllUsersCommandHandler_WhenValuesAreValid_ShouldRemoveRoleFromAllUsers()
        {
            // Arrange
            User newUser1 = UserTestUtility.ValidUser;
            User newUser2 = UserTestUtility.ValidUser;
            Role newRole = RoleTestUtility.ValidRole;
            newRole.AssignToUser(newUser1);
            newRole.AssignToUser(newUser2);

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser1);
            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser2);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new RemoveRoleFromAllUsersCommandRequest()
            {
                RoleId = newRole.Id
            };

            // Act
            var result = await _removeRoleFromAllUsersCommandHandler.Handle(request, default);
            var role = await _unitofWorkFixture.UnitofWork.RoleRepository.GetByIdAsync(newRole.Id, true);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(role!.Users);
        }

        [Fact]
        public async Task RemoveRoleFromAllUsersCommandHandler_WhenRoleIsNotFound_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            var request = new RemoveRoleFromAllUsersCommandRequest()
            {
                RoleId = Guid.NewGuid()
            };

            // Act
            var result = await _removeRoleFromAllUsersCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task RemoveRoleFromAllUsersCommandHandler_WhenRoleIsNotAssignToUsers_ShouldReturnTrue()
        {
            // Arrange
            Role newRole = RoleTestUtility.ValidRole;

            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new RemoveRoleFromAllUsersCommandRequest()
            {
                RoleId = newRole.Id
            };

            // Act
            var result = await _removeRoleFromAllUsersCommandHandler.Handle(request, default);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
