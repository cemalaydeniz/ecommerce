using ecommerce.Application.Validations.UserValidations;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;

namespace ecommerce.ApplicationUnitTest.Validations.UserValidations
{
    public class UserNameValidationTest
    {
        private UserNameValidation _validation;
        public UserNameValidationTest()
        {
            _validation = new UserNameValidation();
        }

        [Theory]
        [MemberData(nameof(UserTestUtility.ValidNames), MemberType = typeof(UserTestUtility))]
        public void ValidateUserName_WhenItIsValid_ShouldReturnNoError(string userName)
        {
            // Pre-condition
            if (string.IsNullOrWhiteSpace(userName))
                return;

            // Act
            var result = _validation.Validate(userName);

            // Assert
            Assert.Empty(result.Errors);
        }

        [Theory]
        [MemberData(nameof(UserTestUtility.InvalidNames), MemberType = typeof(UserTestUtility))]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidateUserName_WhenItIsInvalid_ShouldReturnError(string userName)
        {
            // Act
            var result = _validation.Validate(userName);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
