using ecommerce.Application.Features.Queries.GetUserProfile;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Queries.GetUserProfile
{
    public class GetUserProfileQueryHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private GetUserProfileQueryHandler _getUserProfileQueryHandler;

        public GetUserProfileQueryHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _getUserProfileQueryHandler = new GetUserProfileQueryHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task GetUserProfileQueryHandler_WhenValuesAreValid_ShouldReturnUser()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new GetUserProfileQueryRequest()
            {
                UserId = newUser.Id
            };

            // Act
            var result = await _getUserProfileQueryHandler.Handle(request, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(newUser, result.Response!.User);
        }

        [Fact]
        public async Task GetUserProfileQueryHandler_WhenUserDoesNotExist_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            var request = new GetUserProfileQueryRequest()
            {
                UserId = Guid.NewGuid()
            };

            // Act
            var result = await _getUserProfileQueryHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task GetUserProfileQueryHandler_WhenUserIsSoftDeleted_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            newUser.Delete();

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new GetUserProfileQueryRequest()
            {
                UserId = newUser.Id
            };

            // Act
            var result = await _getUserProfileQueryHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }
    }
}
