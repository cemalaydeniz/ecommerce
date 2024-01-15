using ecommerce.API.Models.RoleController;
using ecommerce.Application.Features.Commands.CreateRole;
using ecommerce.DomainUnitTest.Aggregates.RoleAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.RoleController
{
    public class CreateRoleMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public CreateRoleMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromModelToRequest_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new CreateRoleModel()
            {
                Name = RoleTestUtility.ValidName
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<CreateRoleCommandRequest>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.Name, result.Name);
        }
    }
}
