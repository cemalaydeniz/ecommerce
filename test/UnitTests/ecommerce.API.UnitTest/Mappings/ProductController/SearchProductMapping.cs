using ecommerce.API.Dtos.ProductController;
using ecommerce.Application.Features.Queries.SearchProducts;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.ProductController
{
    public class SearchProductMapping : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public SearchProductMapping(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromResponseToDto_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var response = new SearchProductsQueryResponse()
            {
                Products = new List<Product>()
                {
                    ProductTestUtility.ValidProduct
                }
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<SearchProductDto>(response);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(response.Products[0].Name, result.Result[0].Name);
            Assert.Equal(response.Products[0].Description, result.Result[0].Description);
            Assert.Equal(response.Products[0].Prices[0].CurrencyCode, result.Result[0].Prices[0].CurrencyCode);
            Assert.Equal(response.Products[0].Prices[0].Amount, result.Result[0].Prices[0].Amount);
        }
    }
}
