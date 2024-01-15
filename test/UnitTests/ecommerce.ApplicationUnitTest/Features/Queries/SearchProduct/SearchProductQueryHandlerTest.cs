using ecommerce.Application.Features.Queries.SearchProducts;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Queries.SearchProduct
{
    public class SearchProductQueryHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private SearchProductsQueryHandler _searchProductQueryHandler;

        public SearchProductQueryHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _searchProductQueryHandler = new SearchProductsQueryHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task SearchProductQueryHandler_WhenValusAreValid_ShouldReturnProducts()
        {
            // Arrange
            Product product1 = ProductTestUtility.ValidProduct;
            Product product2 = ProductTestUtility.ValidProduct;
            Product product3 = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(product1);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(product2);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(product3);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new SearchProductsQueryRequest()
            {
                Name = ProductTestUtility.ValidName,
                Page = 1
            };

            // Act
            var result = await _searchProductQueryHandler.Handle(request, default);

            // Assert
            Assert.NotEmpty(result.Products);
        }

        [Fact]
        public async Task SearchProductQueryHandler_WhenProductIsSoftDeleted_ShouldNotReturnProduct()
        {
            // Arrange
            Product product = ProductTestUtility.ValidProduct;
            product.UpdateName("abc");
            product.Delete();
            product.ClearDomainEvents();

            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(product);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new SearchProductsQueryRequest()
            {
                Name = "abc",
                Page = 1
            };

            // Act
            var result = await _searchProductQueryHandler.Handle(request, default);

            // Assert
            Assert.Empty(result.Products);
        }
    }
}
