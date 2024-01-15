using ecommerce.Application.Features.Commands.AssignRoleToUser;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.RoleAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.AssignRoleToUser
{
    public class AssignRoleToUserCommandHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private AssignRoleToUserCommandHandler _assignRoleToUserCommandHandler;

        public AssignRoleToUserCommandHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _assignRoleToUserCommandHandler = new AssignRoleToUserCommandHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task AssignRoleToUserCommandHandler_WhenValuesAreValid_ShouldAssignRoleToUser()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Role newRole = RoleTestUtility.ValidRole;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new AssignRoleToUserCommandRequest()
            {
                RoleId = newRole.Id,
                UserId = newUser.Id
            };

            // Act
            var result = await _assignRoleToUserCommandHandler.Handle(request, default);
            var user = await _unitofWorkFixture.UnitofWork.UserRepository.GetByIdAsync(newUser.Id, true, false);
            var role = await _unitofWorkFixture.UnitofWork.RoleRepository.GetByIdAsync(newRole.Id, true);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(user!.Roles.FirstOrDefault(r => r == newRole));
            Assert.NotNull(role!.Users.FirstOrDefault(u => u == newUser));
        }

        [Fact]
        public async Task AssignRoleToUserCommandHandler_WhenRoleIsNotFound_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new AssignRoleToUserCommandRequest()
            {
                RoleId = Guid.NewGuid(),
                UserId = newUser.Id
            };

            // Act
            var result = await _assignRoleToUserCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task AssignRoleToUserCommandHandler_WhenUserIsNotFound_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            Role newRole = RoleTestUtility.ValidRole;

            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new AssignRoleToUserCommandRequest()
            {
                RoleId = newRole.Id,
                UserId = Guid.NewGuid()
            };

            // Act
            var result = await _assignRoleToUserCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task AssignRoleToUserCommandHandler_WhenUserIsSoftDeleted_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            newUser.Delete();
            Role newRole = RoleTestUtility.ValidRole;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new AssignRoleToUserCommandRequest()
            {
                RoleId = newRole.Id,
                UserId = newUser.Id
            };

            // Act
            var result = await _assignRoleToUserCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task AssignRoleToUserCommandHandler_WhenRoleIsAlreadyAssigned_ShouldReturnTrue()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Role newRole = RoleTestUtility.ValidRole;
            newRole.AssignToUser(newUser);

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new AssignRoleToUserCommandRequest()
            {
                RoleId = newRole.Id,
                UserId = newUser.Id
            };

            await _assignRoleToUserCommandHandler.Handle(request, default);

            // Act
            var result = await _assignRoleToUserCommandHandler.Handle(request, default);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
