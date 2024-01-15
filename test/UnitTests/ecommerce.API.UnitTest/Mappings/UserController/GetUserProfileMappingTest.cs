using ecommerce.API.Dtos.UserController;
using ecommerce.Application.Features.Queries.GetUserProfile;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.UserController
{
    public class GetUserProfileMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public GetUserProfileMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromResponseToDto_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var response = new GetUserProfileQueryResponse()
            {
                User = UserTestUtility.ValidUser
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<GetUserProfileDto>(response);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(response.User.Email, result.Email);
            Assert.Equal(response.User.Name, result.Name);
            Assert.Equal(response.User.PhoneNumber, result.PhoneNumber);
            Assert.Equal(response.User.Addresses, result.Addresses);
        }
    }
}
