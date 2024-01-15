using ecommerce.Application.Features.Commands.RemoveRoleFromUser;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.RoleAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.RemoveRoleFromUser
{
    public class RemoveRoleFromUserCommandHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private RemoveRoleFromUserCommandHandler _removeRoleFromUserCommandHandler;

        public RemoveRoleFromUserCommandHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _removeRoleFromUserCommandHandler = new RemoveRoleFromUserCommandHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task RemoveRoleFromUserCommandHandler_WhenValuesAreValid_ShouldRemoveRoleFromUser()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Role newRole = RoleTestUtility.ValidRole;
            newRole.AssignToUser(newUser);

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new RemoveRoleFromUserCommandRequest()
            {
                RoleId = newRole.Id,
                UserId = newUser.Id
            };

            // Act
            var result = await _removeRoleFromUserCommandHandler.Handle(request, default);
            var user = await _unitofWorkFixture.UnitofWork.UserRepository.GetByIdAsync(newUser.Id, true, false);
            var role = await _unitofWorkFixture.UnitofWork.RoleRepository.GetByIdAsync(newRole.Id, true);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Null(user!.Roles.FirstOrDefault(r => r == newRole));
            Assert.Null(role!.Users.FirstOrDefault(u => u == newUser));
        }

        [Fact]
        public async Task RemoveRoleFromUserCommandHandler_WhenRoleIsNotFound_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new RemoveRoleFromUserCommandRequest()
            {
                RoleId = Guid.NewGuid(),
                UserId = newUser.Id
            };

            // Act
            var result = await _removeRoleFromUserCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task RemoveRoleFromUserCommandHandler_WhenUserIsNotFound_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            Role newRole = RoleTestUtility.ValidRole;

            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new RemoveRoleFromUserCommandRequest()
            {
                RoleId = newRole.Id,
                UserId = Guid.NewGuid()
            };

            // Act
            var result = await _removeRoleFromUserCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task RemoveRoleFromUserCommandHandler_WhenUserIsSoftDeleted_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            newUser.Delete();
            Role newRole = RoleTestUtility.ValidRole;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new RemoveRoleFromUserCommandRequest()
            {
                RoleId = newRole.Id,
                UserId = newUser.Id
            };

            // Act
            var result = await _removeRoleFromUserCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task RemoveRoleFromUserCommandHandler_WhenUserDoesNotHaveRole_ShouldReturnTrue()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Role newRole = RoleTestUtility.ValidRole;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new RemoveRoleFromUserCommandRequest()
            {
                RoleId = newRole.Id,
                UserId = newUser.Id
            };

            // Act
            var result = await _removeRoleFromUserCommandHandler.Handle(request, default);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
