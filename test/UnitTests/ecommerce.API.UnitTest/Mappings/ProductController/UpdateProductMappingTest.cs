using ecommerce.API.Models.ProductController;
using ecommerce.Application.Features.Commands.UpdateProduct;
using ecommerce.Application.Models.ValueObjects;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;
using ecommerce.DomainUnitTest.Common.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.ProductController
{
    public class UpdateProductMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public UpdateProductMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromModelToRequest_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new UpdateProductModel()
            {
                NewName = ProductTestUtility.ValidName,
                NewDescription = ProductTestUtility.ValidDescription,
                CurrencyCodesToRemove = new List<string>() { MoneyTestUtility.ValidCurrencyCode },
                PricesToUpdateOrAdd = new List<MoneyModel>()
                {
                    new MoneyModel()
                    {
                        CurrencyCode = MoneyTestUtility.ValidCurrencyCode,
                        Amount = MoneyTestUtility.ValidAmount
                    }
                }
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<UpdateProductCommandRequest>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.NewName, result.NewName);
            Assert.Equal(model.NewDescription, result.NewDescription);
            Assert.Equal(model.CurrencyCodesToRemove[0], result.CurrencyCodesToRemove![0]);
            Assert.Equal(model.PricesToUpdateOrAdd[0].CurrencyCode, result.PricesToUpdateOrAdd![0].CurrencyCode);
            Assert.Equal(model.PricesToUpdateOrAdd[0].Amount, result.PricesToUpdateOrAdd[0].Amount);
        }
    }
}
