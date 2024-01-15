using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.Persistence.UnitTest.Authentication
{
    public class JwtTokenServiceTest : IClassFixture<JwtTokenServiceFixture>
    {
        private readonly JwtTokenServiceFixture _jwtTokenServiceFixture;

        public JwtTokenServiceTest(JwtTokenServiceFixture jwtTokenServiceFixture)
        {
            _jwtTokenServiceFixture = jwtTokenServiceFixture;
        }

        [Fact]
        public void GenerateNewToken_WhenUserIsValid_ShouldReturnValidToken()
        {
            // Act
            var result = _jwtTokenServiceFixture.JwtTokenService.GenerateToken(UserTestUtility.ValidUser, true);

            // Assert
            Assert.NotNull(result.AccessToken);
            Assert.NotNull(result.RefreshToken);
            Assert.True(result.RefreshTokenExpirationDate > DateTime.UtcNow);
        }
    }
}
