using ecommerce.Application.Validations.ValueObjectValidations;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;

namespace ecommerce.ApplicationUnitTest.Validations.ValueObjectValidations
{
    public class UserAddressTitleValidationTest
    {
        private UserAddressTitleValidation _validation;
        public UserAddressTitleValidationTest()
        {
            _validation = new UserAddressTitleValidation();
        }

        [Theory]
        [MemberData(nameof(UserAddressTestUtility.ValidTitles), MemberType = typeof(UserAddressTestUtility))]
        public void ValidateUserAddressTitle_WhenItIsValid_ShouldReturnNoError(string title)
        {
            // Pre-condition
            if (string.IsNullOrWhiteSpace(title))
                return;

            // Act
            var result = _validation.Validate(title);

            // Assert
            Assert.Empty(result.Errors);
        }

        [Theory]
        [MemberData(nameof(UserAddressTestUtility.InvalidTitles), MemberType = typeof(UserAddressTestUtility))]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidateUserAddressTitle_WhenItIsInvalid_ShouldReturnError(string title)
        {
            // Act
            var result = _validation.Validate(title);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
