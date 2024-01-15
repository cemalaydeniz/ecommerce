using ecommerce.Application.Features.Commands.UpdateRole;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.DomainUnitTest.Aggregates.RoleAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.UpdateRole
{
    public class UpdateRoleCommandHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private UpdateRoleCommandHandler _updateRoleCommandHandler;

        public UpdateRoleCommandHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _updateRoleCommandHandler = new UpdateRoleCommandHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async void UpdateRoleCommandHandler_WhenValuesAreValid_ShouldReturnTrueAndUpdateRole()
        {
            // Arrange
            Role newRole = RoleTestUtility.ValidRole;

            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new UpdateRoleCommandRequest()
            {
                RoleId = newRole.Id,
                NewName = RoleTestUtility.ValidName + "a"
            };

            // Act
            var result = await _updateRoleCommandHandler.Handle(request, default);
            var role = await _unitofWorkFixture.UnitofWork.RoleRepository.GetByIdAsync(newRole.Id, false);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(request.NewName, role!.Name);
        }

        [Fact]
        public async void UpdateRoleCommandHandler_WhenRoleDoesNotExist_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            var request = new UpdateRoleCommandRequest()
            {
                RoleId = Guid.NewGuid(),
            };

            // Act
            var result = await _updateRoleCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async void UpdateRoleCommandHandler_WhenRoleNameIsAlreadyInUse_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            Role newRole = RoleTestUtility.ValidRole;
            Role newRole2 = RoleTestUtility.ValidRole;
            newRole2.UpdateName(RoleTestUtility.ValidName + "b");

            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole2);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new UpdateRoleCommandRequest()
            {
                RoleId = newRole.Id,
                NewName = RoleTestUtility.ValidName + "b"
            };

            // Act
            var result = await _updateRoleCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }
    }
}
