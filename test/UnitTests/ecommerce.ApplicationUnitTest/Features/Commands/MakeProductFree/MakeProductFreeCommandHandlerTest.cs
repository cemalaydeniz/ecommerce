using ecommerce.Application.Features.Commands.MakeProductFree;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.MakeProductFree
{
    public class MakeProductFreeCommandHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private MakeProductFreeCommandHandler _makeProductFreeCommandHandler;

        public MakeProductFreeCommandHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _makeProductFreeCommandHandler = new MakeProductFreeCommandHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task MakeProductFreeCommandHandler_WhenValuesAreValid_ShouldMakeProductFreeofCharge()
        {
            // Arrange
            Product newProduct = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new MakeProductFreeCommandRequest()
            {
                ProductId = newProduct.Id
            };

            // Act
            var result = await _makeProductFreeCommandHandler.Handle(request, default);
            var product = await _unitofWorkFixture.UnitofWork.ProductRepository.GetByIdAsync(newProduct.Id, false);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(Money.Zero, product!.Prices[0]);
        }

        [Fact]
        public async Task MakeProductFreeCommandHandler_WhenProductDoesNotExist_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            var request = new MakeProductFreeCommandRequest()
            {
                ProductId = Guid.NewGuid()
            };

            // Act
            var result = await _makeProductFreeCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task MakeProductFreeCommandHandler_WhenProductIsAlreadyFree_ShouldReturnTrue()
        {
            // Arrange
            Product newProduct = ProductTestUtility.ValidProduct;
            newProduct.MakeFree();

            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new MakeProductFreeCommandRequest()
            {
                ProductId = newProduct.Id
            };

            // Act
            var result = await _makeProductFreeCommandHandler.Handle(request, default);
            var product = await _unitofWorkFixture.UnitofWork.ProductRepository.GetByIdAsync(newProduct.Id, false);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
