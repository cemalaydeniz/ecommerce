using ecommerce.Application.Features.Commands.UpdateCredentials;
using ecommerce.ApplicationUnitTest.Utilities;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.UpdateCredentials
{
    public class UpdateCredentialsCommandHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private UpdateCredentialsCommandHandler _updateCredentialsCommandHandler;

        public UpdateCredentialsCommandHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _updateCredentialsCommandHandler = new UpdateCredentialsCommandHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task UpdateCredentialsCommandHandler_WhenValuesAreValid_ShouldUpdateCredentialsAndClearRefreshToken()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new UpdateCredentialsCommandRequest()
            {
                UserId = newUser.Id,
                CurrentPassword = PasswordTestUtility.ValidPassword,
                NewEmail = $"a{UserTestUtility.ValidEmail}",
                NewPassword = PasswordTestUtility.ValidPassword + "a",
            };

            // Act
            var result = await _updateCredentialsCommandHandler.Handle(request, default);
            var user = await _unitofWorkFixture.UnitofWork.UserRepository.GetByIdAsync(newUser.Id, false, false);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(request.NewEmail, user!.Email);
            Assert.True(BCrypt.Net.BCrypt.Verify(request.NewPassword, user.PasswordHashed));
            Assert.Null(user.RefreshToken);
        }

        [Fact]
        public async Task UpdateCredentialsCommandHandler_WhenUserNotFound_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            var request = new UpdateCredentialsCommandRequest()
            {
                UserId = Guid.NewGuid()
            };

            // Act
            var result = await _updateCredentialsCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task UpdateCredentialsCommandHandler_WhenPasswordIsWrong_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new UpdateCredentialsCommandRequest()
            {
                UserId = newUser.Id,
                CurrentPassword = PasswordTestUtility.ValidPassword + "a"
            };

            // Act
            var result = await _updateCredentialsCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task UpdateCredentialsCommandHandler_WhenEmailAlreadyInUse_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            User newUser2 = UserTestUtility.ValidUser;
            newUser2.UpdateEmail($"b{UserTestUtility.ValidEmail}");

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser2);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new UpdateCredentialsCommandRequest()
            {
                UserId = newUser.Id,
                CurrentPassword = PasswordTestUtility.ValidPassword,
                NewEmail = $"b{UserTestUtility.ValidEmail}",
                NewPassword = PasswordTestUtility.ValidPassword + "a",
            };

            // Act
            var result = await _updateCredentialsCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }
    }
}
