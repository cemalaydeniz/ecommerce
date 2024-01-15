using ecommerce.API.Dtos.RoleController;
using ecommerce.Application.Features.Queries.GetRole;
using ecommerce.DomainUnitTest.Aggregates.RoleAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.RoleController
{
    public class GetRoleMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public GetRoleMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromResponseToDto_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var response = new GetRoleQueryResponse()
            {
                Role = RoleTestUtility.ValidRole
            };
            response.Role.AssignToUser(UserTestUtility.ValidUser);

            // Act
            var result = _autoMapperFixture.Mapper.Map<GetRoleDto>(response);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(response.Role.Name, result.Name);
            Assert.Equal(UserTestUtility.ValidEmail, result.Users[0].Email);
            Assert.Equal(UserTestUtility.ValidName, result.Users[0].Name);
        }
    }
}
