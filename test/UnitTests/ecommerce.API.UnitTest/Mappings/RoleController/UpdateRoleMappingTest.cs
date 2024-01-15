using ecommerce.API.Models.RoleController;
using ecommerce.Application.Features.Commands.UpdateRole;
using ecommerce.DomainUnitTest.Aggregates.RoleAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.RoleController
{
    public class UpdateRoleMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public UpdateRoleMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromModelToRequest_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new UpdateRoleModel()
            {
                NewName = RoleTestUtility.ValidName
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<UpdateRoleCommandRequest>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.NewName, result.NewName);
        }
    }
}
