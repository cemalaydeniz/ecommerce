using ecommerce.Application.Features.Queries.InitiatePayment;
using ecommerce.Application.Validations.PaymentValidations;
using ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities;

namespace ecommerce.ApplicationUnitTest.Validations.PaymentValidations
{
    public class PaymentItemValidationTest
    {
        private PaymentItemValidation _validation;
        public PaymentItemValidationTest()
        {
            _validation = new PaymentItemValidation();
        }

        [Theory]
        [MemberData(nameof(OrderItemTestUtility.ValidQuantities), MemberType = typeof(OrderItemTestUtility))]
        public void ValidatePaymenItemValue_WhenItemIsValid_ShouldReturnNoError(int quantity)
        {
            // Arrange
            var item = new InitiatePaymentQueryRequest.Item()
            {
                ProductId = Guid.NewGuid(),
                Quantity = quantity
            };

            // Act
            var result = _validation.Validate(item);

            // Assert
            Assert.Empty(result.Errors);
        }

        [Theory]
        [MemberData(nameof(OrderItemTestUtility.InvalidQuantities), MemberType = typeof(OrderItemTestUtility))]
        public void ValidatePaymenItemValue_WhenItemIsInvalid_ShouldReturnError(int quantity)
        {
            // Arrange
            var item = new InitiatePaymentQueryRequest.Item()
            {
                ProductId = Guid.NewGuid(),
                Quantity = quantity
            };

            // Act
            var result = _validation.Validate(item);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
