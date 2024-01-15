using ecommerce.Application.Validations.UserValidations;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;

namespace ecommerce.ApplicationUnitTest.Validations.UserValidations
{
    public class PhoneNumberValidationTest
    {
        private PhoneNumberValidation _validation;
        public PhoneNumberValidationTest()
        {
            _validation = new PhoneNumberValidation();
        }

        [Theory]
        [MemberData(nameof(UserTestUtility.ValidPhoneNumbers), MemberType = typeof(UserTestUtility))]
        public void ValidatePhoneNumber_WhenItIsValid_ShouldReturnNoError(string phoneNumber)
        {
            // Pre-condition
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return;

            // Act
            var result = _validation.Validate(phoneNumber);

            // Assert
            Assert.Empty(result.Errors);
        }

        [Theory]
        [MemberData(nameof(UserTestUtility.InvalidPhoneNumbers), MemberType = typeof(UserTestUtility))]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidatePhoneNumber_WhenItIsInvalid_ShouldReturnError(string phoneNumber)
        {
            // Act
            var result = _validation.Validate(phoneNumber);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
