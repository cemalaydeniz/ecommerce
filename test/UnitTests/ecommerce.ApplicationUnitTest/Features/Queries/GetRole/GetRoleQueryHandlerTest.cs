using ecommerce.Application.Features.Queries.GetRole;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.RoleAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Queries.GetRole
{
    public class GetRoleQueryHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private GetRoleQueryHandler _getRoleQueryHandler;

        public GetRoleQueryHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _getRoleQueryHandler = new GetRoleQueryHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task GetRoleQueryHandler_WhenValuesAreValid_ShouldReturnRole()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Role newRole = RoleTestUtility.ValidRole;
            newRole.AssignToUser(newUser);

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new GetRoleQueryRequest()
            {
                RoleId = newRole.Id,
                GetUsers = true
            };

            // Act
            var result = await _getRoleQueryHandler.Handle(request, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(newRole, result.Response!.Role);
            Assert.True((bool)result.Response!.Role.Users.Any(u => u == newUser));
        }

        [Fact]
        public async Task GetRoleQueryHandler_WhenRoleDoesNotExist_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            var request = new GetRoleQueryRequest()
            {
                RoleId = Guid.NewGuid(),
                GetUsers = true
            };

            // Act
            var result = await _getRoleQueryHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }
    }
}
