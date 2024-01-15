using ecommerce.Application.Validations.PaymentValidations;

namespace ecommerce.ApplicationUnitTest.Validations.PaymentValidations
{
    public class PaymentHttpBodyValidationTest
    {
        private PaymentHttpBodyValidation _paymentHttpBodyValidation;
        public PaymentHttpBodyValidationTest()
        {
            _paymentHttpBodyValidation = new PaymentHttpBodyValidation();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidateHttpBodyValue_WhenValueIsInvalid_ShouldReturnError(string header)
        {
            // Act
            var result = _paymentHttpBodyValidation.Validate(header);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
