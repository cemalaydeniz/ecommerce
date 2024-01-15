using ecommerce.API.Dtos.UserController;
using ecommerce.API.Models.UserController;
using ecommerce.Application.Authentication;
using ecommerce.Application.Features.Commands.SignIn;
using ecommerce.ApplicationUnitTest.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.UserController
{
    public class SignInMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public SignInMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromModelToRequest_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new SignInModel()
            {
                Email = UserTestUtility.ValidEmail,
                Password = PasswordTestUtility.ValidPassword
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<SignInCommandRequest>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.Email, result.Email);
            Assert.Equal(model.Password, result.Password);
        }

        [Fact]
        public void MapFromResponseToDto_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var response = new SignInCommandResponse()
            {
                Token = new JwtToken()
                {
                    AccessToken = TokenTestUtility.ValidTokenValue,
                    RefreshToken = TokenTestUtility.ValidTokenValue,
                    RefreshTokenExpirationDate = TokenTestUtility.ValidExpiresAt
                }
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<SignInDto>(response);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(response.Token.AccessToken, result.AccessToken);
        }
    }
}
