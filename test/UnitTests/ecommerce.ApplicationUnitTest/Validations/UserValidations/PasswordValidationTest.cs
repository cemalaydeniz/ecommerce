using ecommerce.Application.Validations.UserValidations;
using ecommerce.ApplicationUnitTest.Utilities;

namespace ecommerce.ApplicationUnitTest.Validations.UserValidations
{
    public class PasswordValidationTest
    {
        private PasswordValidation _validation;
        public PasswordValidationTest()
        {
            _validation = new PasswordValidation();
        }

        [Theory]
        [MemberData(nameof(PasswordTestUtility.ValidPasswords), MemberType = typeof(PasswordTestUtility))]
        public void ValidatePassword_WhenItIsValid_ShouldReturnNoError(string password)
        {
            // Act
            var result = _validation.Validate(password);

            // Assert
            Assert.Empty(result.Errors);
        }

        [Theory]
        [MemberData(nameof(PasswordTestUtility.InvalidPasswords), MemberType = typeof(PasswordTestUtility))]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidatePassword_WhenItIsInvalid_ShouldReturnError(string password)
        {
            // Act
            var result = _validation.Validate(password);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
