using ecommerce.Application.Features.Commands.CreateRole;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.DomainUnitTest.Aggregates.RoleAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.CreateRole
{
    public class CreateRoleCommandHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private CreateRoleCommandHandler _createRoleCommandHandler;

        public CreateRoleCommandHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _createRoleCommandHandler = new CreateRoleCommandHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task CreateRoleCommandHandler_WhenValuesAreValid_ShouldCreateNewRole()
        {
            // Arrange
            var request = new CreateRoleCommandRequest()
            {
                Name = RoleTestUtility.ValidName + "a"
            };

            // Act
            var result = await _createRoleCommandHandler.Handle(request, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(await _unitofWorkFixture.UnitofWork.RoleRepository.GetByIdAsync(result.Response!.RoleId, false));
        }

        [Fact]
        public async Task CreateRoleCommandHandler_WhenRoleAlreadyExists_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            Role newRole = RoleTestUtility.ValidRole;

            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new CreateRoleCommandRequest()
            {
                Name = RoleTestUtility.ValidName
            };

            // Act
            var result = await _createRoleCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }
    }
}
