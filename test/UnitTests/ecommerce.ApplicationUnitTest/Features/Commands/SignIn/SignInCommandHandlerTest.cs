using ecommerce.Application.Features.Commands.SignIn;
using ecommerce.ApplicationUnitTest.Utilities;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;
using Microsoft.Extensions.Logging;
using Moq;

namespace ecommerce.ApplicationUnitTest.Features.Commands.SignIn
{
    public class SignInCommandHandlerTest : IClassFixture<UnitofWorkFixture>, IClassFixture<JwtTokenServiceFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;
        private readonly JwtTokenServiceFixture _jwtTokenServiceFixture;
        private readonly Mock<ILogger<SignInCommandHandler>> _mockLogger;

        private SignInCommandHandler _signInCommandHandler;

        public SignInCommandHandlerTest(UnitofWorkFixture unitofWorkFixture,
            JwtTokenServiceFixture jwtTokenServiceFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;
            _jwtTokenServiceFixture = jwtTokenServiceFixture;
            _mockLogger = new Mock<ILogger<SignInCommandHandler>>();

            _signInCommandHandler = new SignInCommandHandler(_unitofWorkFixture.UnitofWork,
                _jwtTokenServiceFixture.JwtTokenService,
                _mockLogger.Object);
        }

        [Fact]
        public async Task SignInQueryHandler_WhenValuesAreValid_ShouldSignInAndReturnJwtToken()
        {
            // Arrange
            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(new User(UserTestUtility.ValidEmail,
                BCrypt.Net.BCrypt.HashPassword(PasswordTestUtility.ValidPassword), null, null, null));
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new SignInCommandRequest()
            {
                Email = UserTestUtility.ValidEmail,
                Password = PasswordTestUtility.ValidPassword
            };

            // Act
            var result = await _signInCommandHandler.Handle(request, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Response!.Token.AccessToken);
            Assert.NotNull(result.Response.Token.RefreshToken);
            Assert.True(result.Response.Token.RefreshTokenExpirationDate > DateTime.UtcNow);
        }

        [Fact]
        public async Task SignInQueryHandler_WhenUserNotFound_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            var request = new SignInCommandRequest()
            {
                Email = UserTestUtility.ValidEmail + "a",
                Password = PasswordTestUtility.ValidPassword
            };

            // Act
            var result = await _signInCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task SignInQueryHandler_WhenPasswordDoesNotMatch_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(new User(UserTestUtility.ValidEmail,
                BCrypt.Net.BCrypt.HashPassword(PasswordTestUtility.ValidPassword), null, null, null));
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new SignInCommandRequest()
            {
                Email = UserTestUtility.ValidEmail,
                Password = PasswordTestUtility.ValidPassword + "a"
            };

            // Act
            var result = await _signInCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }
    }
}
