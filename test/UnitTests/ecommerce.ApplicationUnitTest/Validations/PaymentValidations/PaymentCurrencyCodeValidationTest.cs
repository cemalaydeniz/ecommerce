using ecommerce.Application.Validations.PaymentValidations;
using ecommerce.DomainUnitTest.Common.Utilities;

namespace ecommerce.ApplicationUnitTest.Validations.PaymentValidations
{
    public class PaymentCurrencyCodeValidationTest
    {
        private PaymentCurrencyCodeValidation _validation;
        public PaymentCurrencyCodeValidationTest()
        {
            _validation = new PaymentCurrencyCodeValidation();
        }

        [Theory]
        [MemberData(nameof(MoneyTestUtility.ValidCurrencyCodes), MemberType = typeof(MoneyTestUtility))]
        public void ValidatePaymentCurrencyCodeValue_WhenValueIsValid_ShouldReturnNoError(string currencyCode)
        {
            // Pre-condition
            if (string.IsNullOrWhiteSpace(currencyCode))
                return;

            // Act
            var result = _validation.Validate(currencyCode);

            // Assert
            Assert.Empty(result.Errors);
        }

        [Theory]
        [MemberData(nameof(MoneyTestUtility.InvalidCurrencyCodes), MemberType = typeof(MoneyTestUtility))]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidatePaymentCurrencyCodeValue_WhenValueIsInvalid_ShouldReturnError(string currencyCode)
        {
            // Act
            var result = _validation.Validate(currencyCode);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
