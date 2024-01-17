using ecommerce.API.Models.ProductController;
using ecommerce.Application.Features.Commands.CreateProduct;
using ecommerce.Application.Models.ValueObjects;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;
using ecommerce.DomainUnitTest.Common.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.ProductController
{
    public class CreateProductMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public CreateProductMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromModelToRequest_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new CreateProductModel()
            {
                Name = ProductTestUtility.ValidName,
                Prices = new List<MoneyModel>() { new MoneyModel() 
                {
                    CurrencyCode = MoneyTestUtility.ValidCurrencyCode,
                    Amount = MoneyTestUtility.ValidAmount
                } },
                Description = ProductTestUtility.ValidDescription
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<CreateProductCommandRequest>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.Name, result.Name);
            Assert.Equal(model.Description, result.Description);
            Assert.Equal(model.Prices[0].CurrencyCode, result.Prices[0].CurrencyCode);
            Assert.Equal(model.Prices[0].Amount, result.Prices[0].Amount);
        }
    }
}
