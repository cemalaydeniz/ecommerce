using ecommerce.Application.Validations.ProductValidations;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;

namespace ecommerce.ApplicationUnitTest.Validations.ProductValidations
{
    public class ProductNameValidationTest
    {
        private ProductNameValidation _validation;
        public ProductNameValidationTest()
        {
            _validation = new ProductNameValidation();
        }

        [Theory]
        [MemberData(nameof(ProductTestUtility.ValidNames), MemberType = typeof(ProductTestUtility))]
        public void ValidateProductNameValue_WhenValueIsValid_ShouldReturnNoError(string productName)
        {
            // Pre-condition
            if (string.IsNullOrWhiteSpace(productName))
                return;

            // Act
            var result = _validation.Validate(productName);

            // Assert
            Assert.Empty(result.Errors);
        }

        [Theory]
        [MemberData(nameof(ProductTestUtility.InvalidNames), MemberType = typeof(ProductTestUtility))]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidateProductNameValue_WhenValueIsInvalid_ShouldReturnError(string productName)
        {
            // Act
            var result = _validation.Validate(productName);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
