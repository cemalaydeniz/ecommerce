using ecommerce.Application.Validations.ProductValidations;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;

namespace ecommerce.ApplicationUnitTest.Validations.ProductValidations
{
    public class ProductDescriptionValidationTest
    {
        private ProductDescriptionValidation _validation;
        public ProductDescriptionValidationTest()
        {
            _validation = new ProductDescriptionValidation();
        }

        [Theory]
        [MemberData(nameof(ProductTestUtility.ValidDescriptions), MemberType = typeof(ProductTestUtility))]
        public void ValidateProductDescriptionValue_WhenValueIsValid_ShouldReturnNoError(string productName)
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
        [MemberData(nameof(ProductTestUtility.InvalidDescriptions), MemberType = typeof(ProductTestUtility))]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidateProductDescriptionValue_WhenValueIsInvalid_ShouldReturnError(string productName)
        {
            // Act
            var result = _validation.Validate(productName);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
