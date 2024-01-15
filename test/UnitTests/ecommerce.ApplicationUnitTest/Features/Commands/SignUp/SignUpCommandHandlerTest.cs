using ecommerce.Application.Features.Commands.SignUp;
using ecommerce.ApplicationUnitTest.Utilities;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.SignUp
{
    public class SignUpCommandHandlerTest : IClassFixture<UnitofWorkFixture>, IClassFixture<AutoMapperFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;
        private readonly AutoMapperFixture _mapperFixture;

        private SignUpCommandHandler _signUpCommandHandler;

        public SignUpCommandHandlerTest(UnitofWorkFixture unitofWorkFixture,
            AutoMapperFixture mapperFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;
            _mapperFixture = mapperFixture;

            _signUpCommandHandler = new SignUpCommandHandler(_unitofWorkFixture.UnitofWork, _mapperFixture.Mapper);
        }

        [Fact]
        public async Task SignUpCommandHandler_WhenValuesAreValid_ShouldCreateNewUser()
        {
            // Arrange
            var request = new SignUpCommandRequest()
            {
                Email = $"a{UserTestUtility.ValidEmail}",
                Password = PasswordTestUtility.ValidPassword,
                Name = null,
                PhoneNumber = null,
                Address = null
            };

            // Act
            var result = await _signUpCommandHandler.Handle(request, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(await _unitofWorkFixture.UnitofWork.UserRepository.GetByIdAsync(result.Response!.UserId, false, false));
        }

        [Fact]
        public async Task SignUpCommandHandler_WhenUserAlreadyExists_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new SignUpCommandRequest()
            {
                Email = UserTestUtility.ValidEmail,
                Password = PasswordTestUtility.ValidPassword,
                Name = null,
                PhoneNumber = null,
                Address = null
            };

            // Act
            var result = await _signUpCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }
    }
}
