using ecommerce.Application.Features.Commands.SignOut;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.SignOut
{
    public class SignOutCommandHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private SignOutCommandHandler _signOutCommandHandler;

        public SignOutCommandHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _signOutCommandHandler = new SignOutCommandHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task SignOutQueryHandler_WhenSignOutAllDevicesOptionIsTrue_ShouldClearRefreshToken()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            newUser.UpdateRefreshToken(TokenTestUtility.ValidTokenValue, TokenTestUtility.ValidExpiresAt);

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new SignOutCommandRequest()
            {
                UserId = newUser.Id,
                SignOutAllDevices = true
            };

            // Act
            await _signOutCommandHandler.Handle(request, default);
            var user = await _unitofWorkFixture.UnitofWork.UserRepository.GetByIdAsync(newUser.Id, false, false);

            // Assert
            Assert.Null(user!.RefreshToken);
        }
    }
}
