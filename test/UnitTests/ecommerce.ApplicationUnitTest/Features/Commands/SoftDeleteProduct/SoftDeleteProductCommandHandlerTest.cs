using ecommerce.Application.Features.Commands.SoftDeleteProduct;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.SoftDeleteProduct
{
    public class SoftDeleteProductCommandHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private SoftDeleteProductCommandHandler _softDeleteProductCommandHandler;

        public SoftDeleteProductCommandHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _softDeleteProductCommandHandler = new SoftDeleteProductCommandHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task SoftDeleteProductCommandHandler_WhenValuesAreValid_ShouldSoftDeleteProduct()
        {
            // Arrange
            Product newProduct = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new SoftDeleteProductCommandRequest()
            {
                ProductId = newProduct.Id
            };

            // Act
            var result = await _softDeleteProductCommandHandler.Handle(request, default);
            var product = await _unitofWorkFixture.UnitofWork.ProductRepository.GetByIdAsync(newProduct.Id, true);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(product!.IsDeleted);
        }

        [Fact]
        public async Task SoftDeleteUserCommandHandler_WhenProductDoesNotExist_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            var request = new SoftDeleteProductCommandRequest()
            {
                ProductId = Guid.NewGuid()
            };

            // Act
            var result = await _softDeleteProductCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task SoftDeleteUserCommandHandler_WhenProductIsAlreadySoftDeleted_ShouldReturnTrue()
        {
            // Arrange
            Product newProduct = ProductTestUtility.ValidProduct;
            newProduct.Delete();

            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new SoftDeleteProductCommandRequest()
            {
                ProductId = newProduct.Id
            };

            // Act
            var result = await _softDeleteProductCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
