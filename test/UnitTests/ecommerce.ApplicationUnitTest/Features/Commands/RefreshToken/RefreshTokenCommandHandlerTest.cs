using ecommerce.Application.Features.Commands.RefreshToken;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;
using Microsoft.Extensions.Logging;
using Moq;

namespace ecommerce.ApplicationUnitTest.Features.Commands.RefreshToken
{
    public class RefreshTokenCommandHandlerTest : IClassFixture<UnitofWorkFixture>, IClassFixture<JwtTokenServiceFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;
        private readonly JwtTokenServiceFixture _jwtTokenServiceFixture;
        private readonly Mock<ILogger<RefreshTokenCommandHandler>> _mockLogger;

        private RefreshTokenCommandHandler _refreshTokenCommandHandler;

        public RefreshTokenCommandHandlerTest(UnitofWorkFixture unitofWorkFixture,
            JwtTokenServiceFixture jwtTokenServiceFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;
            _jwtTokenServiceFixture = jwtTokenServiceFixture;
            _mockLogger = new Mock<ILogger<RefreshTokenCommandHandler>>();

            _refreshTokenCommandHandler = new RefreshTokenCommandHandler(_unitofWorkFixture.UnitofWork,
                _jwtTokenServiceFixture.JwtTokenService,
                _mockLogger.Object);
        }

        [Fact]
        public async Task RefreshTokenQueryHandler_WhenValuesAreValid_ShouldReturnToken()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            newUser.UpdateRefreshToken(TokenTestUtility.ValidTokenValue, TokenTestUtility.ValidExpiresAt);

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            // Act
            var request = new RefreshTokenCommandRequest()
            {
                UserId = newUser.Id,
                RefreshToken = TokenTestUtility.ValidTokenValue
            };

            var result = await _refreshTokenCommandHandler.Handle(request, default);

            // Aseert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Response!.Token.AccessToken);
            Assert.NotNull(result.Response.Token.RefreshToken);
            Assert.True(result.Response.Token.RefreshTokenExpirationDate > DateTime.UtcNow);
        }

        [Fact]
        public async Task RefreshTokenQueryHandler_WhenUserNotFound_ShouldReturnFalseAndReturnError()
        {
            // Act
            var request = new RefreshTokenCommandRequest()
            {
                UserId = Guid.NewGuid(),
                RefreshToken = TokenTestUtility.ValidTokenValue
            };

            var result = await _refreshTokenCommandHandler.Handle(request, default);

            // Aseert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("abcd")]
        public async Task RefreshTokenQueryHandler_WhenTokenIsNullOrDoesNotMatch_ShouldReturnFalseAndReturnError(string token)
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            newUser.UpdateRefreshToken("abc", TokenTestUtility.ValidExpiresAt);

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            // Act
            var request = new RefreshTokenCommandRequest()
            {
                UserId = newUser.Id,
                RefreshToken = token
            };

            var result = await _refreshTokenCommandHandler.Handle(request, default);

            // Aseert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }
    }
}
