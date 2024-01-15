using ecommerce.API.Dtos.ProductController;
using ecommerce.Application.Features.Queries.GetProduct;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.ProductController
{
    public class GetProductMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public GetProductMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromResponseToDto_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var response = new GetProductQueryResponse()
            {
                Product = ProductTestUtility.ValidProduct
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<GetProductDto>(response);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(response.Product.Name, result.Name);
            Assert.Equal(response.Product.Description, result.Description);
            Assert.Equal(response.Product.Prices[0].CurrencyCode, result.Prices[0].CurrencyCode);
            Assert.Equal(response.Product.Prices[0].Amount, result.Prices[0].Amount);
        }
    }
}
