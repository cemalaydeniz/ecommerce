using ecommerce.Application.Features.Commands.DeleteRole;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.RoleAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.DeleteRole
{
    public class DeleteRoleCommandHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private DeleteRoleCommandHandler _deleteRoleCommandHandler;

        public DeleteRoleCommandHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _deleteRoleCommandHandler = new DeleteRoleCommandHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task DeleteRoleCommandHandler_WhetherRoleExistsOrNot_ShouldDeleteRoleAndRemoveAssignedRoles()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Role newRole = RoleTestUtility.ValidRole;
            newRole.AssignToUser(newUser);

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new DeleteRoleCommandRequest()
            {
                RoleId = newRole.Id
            };

            // Act
            await _deleteRoleCommandHandler.Handle(request, default);
            var role = await _unitofWorkFixture.UnitofWork.RoleRepository.GetByIdAsync(request.RoleId, false);
            var user = await _unitofWorkFixture.UnitofWork.UserRepository.GetByIdAsync(newUser.Id, true, false);

            // Assert
            Assert.Null(role);
            Assert.Empty(user!.Roles);
        }
    }
}
