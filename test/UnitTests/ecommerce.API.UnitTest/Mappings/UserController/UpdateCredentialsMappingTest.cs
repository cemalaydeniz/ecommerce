using ecommerce.API.Models.UserController;
using ecommerce.Application.Features.Commands.UpdateCredentials;
using ecommerce.ApplicationUnitTest.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.UserController
{
    public class UpdateCredentialsMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public UpdateCredentialsMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromModelToRequest_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new UpdateCredentialsModel()
            {
                NewEmail = UserTestUtility.ValidEmail,
                NewPassword = PasswordTestUtility.ValidPassword
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<UpdateCredentialsCommandRequest>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.NewEmail, result.NewEmail);
            Assert.Equal(model.NewPassword, result.NewPassword);
        }
    }
}
