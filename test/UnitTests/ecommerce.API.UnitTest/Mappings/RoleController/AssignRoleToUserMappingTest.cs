using ecommerce.API.Models.RoleController;
using ecommerce.Application.Features.Commands.AssignRoleToUser;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.RoleController
{
    public class AssignRoleToUserMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public AssignRoleToUserMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromModelToRequest_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new AssignRoleToUserModel()
            {
                UserId = Guid.NewGuid()
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<AssignRoleToUserCommandRequest>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.UserId, result.UserId);
        }
    }
}
