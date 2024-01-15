using ecommerce.API.Models.RoleController;
using ecommerce.Application.Features.Commands.RemoveRoleFromUser;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.RoleController
{
    public class RemoveRoleFromUserMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public RemoveRoleFromUserMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromModelToRequest_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new RemoveRoleFromUserModel()
            {
                UserId = Guid.NewGuid()
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<RemoveRoleFromUserCommandRequest>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.UserId, result.UserId);
        }
    }
}
