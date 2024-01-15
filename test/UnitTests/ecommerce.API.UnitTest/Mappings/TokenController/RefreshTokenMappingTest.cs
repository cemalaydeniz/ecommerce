using ecommerce.API.Dtos.TokenController;
using ecommerce.Application.Authentication;
using ecommerce.Application.Features.Commands.RefreshToken;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.TokenController
{
    public class RefreshTokenMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public RefreshTokenMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromResponseToDto_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var response = new RefreshTokenCommandResponse()
            {
                Token = new JwtToken()
                {
                    AccessToken = TokenTestUtility.ValidTokenValue,
                    RefreshToken = TokenTestUtility.ValidTokenValue,
                    RefreshTokenExpirationDate = TokenTestUtility.ValidExpiresAt
                }
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<RefreshTokenDto>(response);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(response.Token.AccessToken, result.AccessToken);
        }
    }
}
