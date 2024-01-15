using ecommerce.Application.Validations.PaymentValidations;

namespace ecommerce.ApplicationUnitTest.Validations.PaymentValidations
{
    public class PaymentHttpHeaderValidationTest
    {
        private PaymentHttpHeaderValidation _paymentHttpHeaderValidation;
        public PaymentHttpHeaderValidationTest()
        {
            _paymentHttpHeaderValidation = new PaymentHttpHeaderValidation();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidateHttpHeaderValue_WhenValueIsInvalid_ShouldReturnError(string header)
        {
            // Act
            var result = _paymentHttpHeaderValidation.Validate(header);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
