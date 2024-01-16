using ecommerce.Application.Validations.OrderValidations;
using ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities;

namespace ecommerce.ApplicationUnitTest.Validations.OrderValidations
{
    public class OrderMessageContentValidationTest
    {
        private OrderMessageContentValidation _validation;
        public OrderMessageContentValidationTest()
        {
            _validation = new OrderMessageContentValidation();
        }

        [Theory]
        [MemberData(nameof(TicketMessageTestUtility.ValidContents), MemberType = typeof(TicketMessageTestUtility))]
        public void ValidateProductDescriptionValue_WhenValueIsValid_ShouldReturnNoError(string content)
        {
            // Pre-condition
            if (string.IsNullOrWhiteSpace(content))
                return;

            // Act
            var result = _validation.Validate(content);

            // Assert
            Assert.Empty(result.Errors);
        }

        [Theory]
        [MemberData(nameof(TicketMessageTestUtility.InvalidContents), MemberType = typeof(TicketMessageTestUtility))]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidateProductDescriptionValue_WhenValueIsInvalid_ShouldReturnError(string content)
        {
            // Act
            var result = _validation.Validate(content);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
