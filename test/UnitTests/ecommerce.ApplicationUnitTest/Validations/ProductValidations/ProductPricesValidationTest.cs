using ecommerce.Application.Models.ValueObjects;
using ecommerce.Application.Validations.ProductValidations;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Validations.ProductValidations
{
    public class ProductPricesValidationTest : IClassFixture<AutoMapperFixture>
    {
        private AutoMapperFixture _autoMapperFixture;

        ProductPricesValidation _validation;
        public ProductPricesValidationTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;

            _validation = new ProductPricesValidation();
        }

        [Theory]
        [MemberData(nameof(ProductTestUtility.ValidPriceLists), MemberType = typeof(ProductTestUtility))]
        public void ValidateProductPricesValue_WhenValueIsValid_ShouldReturnNoError(List<Money> prices)
        {
            // Pre-condition
            if (prices == null || prices.Count == 0)
                return;

            // Arrange
            List<MoneyModel> model = new List<MoneyModel>();
            foreach (var money in prices)
            {
                model.Add(_autoMapperFixture.Mapper.Map<MoneyModel>(money));
            }

            // Act
            var result = _validation.Validate(model);

            // Assert
            Assert.Empty(result.Errors);
        }

        [Theory]
        [MemberData(nameof(ProductTestUtility.InvalidPriceLists), MemberType = typeof(ProductTestUtility))]
        public void ValidateProductPricesValue_WhenValueIsInvalid_ShouldReturnError(List<Money>? prices)
        {
            // Pre-condition
            if (prices == null || prices.Count == 0)
                return;

            // Arrange
            List<MoneyModel> model = new List<MoneyModel>();
            foreach (var money in prices)
            {
                model.Add(_autoMapperFixture.Mapper.Map<MoneyModel>(money));
            }

            // Act
            var result = _validation.Validate(model);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
