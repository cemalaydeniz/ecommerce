using ecommerce.Application.Validations.UserValidations;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;

namespace ecommerce.ApplicationUnitTest.Validations.UserValidations
{
    public class EmailValidationTest
    {
        private EmailValidation _validation;
        public EmailValidationTest()
        {
            _validation = new EmailValidation();
        }

        [Theory]
        [MemberData(nameof(UserTestUtility.ValidEmails), MemberType = typeof(UserTestUtility))]
        public void ValidateEmailValue_WhenItIsValid_ShouldReturnNoError(string email)
        {
            // Pre-condition
            if (string.IsNullOrWhiteSpace(email))
                return;

            // Act
            var result = _validation.Validate(email);

            // Assert
            Assert.Empty(result.Errors);
        }

        [Theory]
        [MemberData(nameof(UserTestUtility.InvalidEmails), MemberType = typeof(UserTestUtility))]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidateEmailValue_WhenItIsInvalid_ShouldReturnError(string email)
        {
            // Act
            var result = _validation.Validate(email);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
