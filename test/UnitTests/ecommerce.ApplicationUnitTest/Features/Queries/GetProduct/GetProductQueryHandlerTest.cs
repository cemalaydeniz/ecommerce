using ecommerce.Application.Features.Queries.GetProduct;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Queries.GetProduct
{
    public class GetProductQueryHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private GetProductQueryHandler _getProductQueryHandler;

        public GetProductQueryHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _getProductQueryHandler = new GetProductQueryHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task GetProductQueryHandler_WhenValuesAreValid_ShouldReturnProduct()
        {
            // Arrange
            Product product = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(product);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new GetProductQueryRequest()
            {
                ProductId = product.Id
            };

            // Act
            var result = await _getProductQueryHandler.Handle(request, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(product, result.Response!.Product);
        }

        [Fact]
        public async Task GetProductQueryHandler_WhenProductDoesNotExist_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            var request = new GetProductQueryRequest()
            {
                ProductId = Guid.NewGuid()
            };

            // Act
            var result = await _getProductQueryHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }
    }
}
