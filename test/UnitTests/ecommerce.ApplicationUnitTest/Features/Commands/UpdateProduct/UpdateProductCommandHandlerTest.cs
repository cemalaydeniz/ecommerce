using ecommerce.Application.Features.Commands.UpdateProduct;
using ecommerce.Application.Models.ValueObjects;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;
using ecommerce.DomainUnitTest.Common.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.UpdateProduct
{
    public class UpdateProductCommandHandlerTest : IClassFixture<UnitofWorkFixture>, IClassFixture<AutoMapperFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;
        private readonly AutoMapperFixture _mapperFixture;

        private UpdateProductCommandHandler _updateProductCommandHandler;

        public UpdateProductCommandHandlerTest(UnitofWorkFixture unitofWorkFixture,
            AutoMapperFixture mapperFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;
            _mapperFixture = mapperFixture;

            _updateProductCommandHandler = new UpdateProductCommandHandler(_unitofWorkFixture.UnitofWork, _mapperFixture.Mapper);
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_WhenValuesAreValid_ShouldReturnTrueAndUpdateProduct()
        {
            // Arrange
            Product newProduct = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new UpdateProductCommandRequest()
            {
                ProductId = newProduct.Id,
                NewName = ProductTestUtility.ValidName + "a",
                NewDescription = ProductTestUtility.ValidDescription + "a",
                CurrencyCodesToRemove = new List<string>() { "ABC" },
                PricesToUpdateOrAdd = new List<MoneyModel>()
                {
                    new MoneyModel()
                    {
                        CurrencyCode = MoneyTestUtility.ValidCurrencyCode,
                        Amount = 1
                    },
                    new MoneyModel()
                    {
                        CurrencyCode = MoneyTestUtility.ValidCurrencyCode[0].ToString(),
                        Amount = MoneyTestUtility.ValidAmount
                    }
                }
            };

            // Act
            var result = await _updateProductCommandHandler.Handle(request, default);
            var product = await _unitofWorkFixture.UnitofWork.ProductRepository.GetByIdAsync(newProduct.Id, false);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(request.NewName, product!.Name);
            Assert.Equal(request.NewDescription, product.Description);
            Assert.False((bool)product.Prices.Any(m => m.CurrencyCode == request.CurrencyCodesToRemove[0]));
            Assert.Equal(request.PricesToUpdateOrAdd[0].Amount, product.Prices.FirstOrDefault(m => m.CurrencyCode == request.PricesToUpdateOrAdd[0].CurrencyCode)!.Amount);
            Assert.True((bool)product.Prices.Any(m => m.CurrencyCode == MoneyTestUtility.ValidCurrencyCode[0].ToString()));
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_WhenProductDoesNotExist_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            var request = new UpdateProductCommandRequest()
            {
                ProductId = Guid.NewGuid()
            };

            // Act
            var result = await _updateProductCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_WhenProductIsFreeAndTryToRemovePrice_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            Product newProduct = ProductTestUtility.ValidFreeProduct;

            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new UpdateProductCommandRequest()
            {
                ProductId = newProduct.Id,
                CurrencyCodesToRemove = new List<string>() { Money.Zero.CurrencyCode },
            };

            // Act
            var result = await _updateProductCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_WhenTryToRemoveAllPrices_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            Product newProduct = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new UpdateProductCommandRequest()
            {
                ProductId = newProduct.Id,
                CurrencyCodesToRemove = new List<string>() { MoneyTestUtility.ValidCurrencyCode, "ABC" }
            };

            // Act
            var result = await _updateProductCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_WhenProductIsFreeAndTryToAddPrice_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            Product newProduct = ProductTestUtility.ValidFreeProduct;

            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new UpdateProductCommandRequest()
            {
                ProductId = newProduct.Id,
                PricesToUpdateOrAdd = new List<MoneyModel>()
                {
                    new MoneyModel()
                    {
                        CurrencyCode = MoneyTestUtility.ValidCurrencyCode[0].ToString(),
                        Amount = MoneyTestUtility.ValidAmount
                    }
                }
            };

            // Act
            var result = await _updateProductCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_WhenProductIsPaidAndTryToAddFreePrice_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            Product newProduct = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new UpdateProductCommandRequest()
            {
                ProductId = newProduct.Id,
                PricesToUpdateOrAdd = new List<MoneyModel>()
                {
                    new MoneyModel()
                    {
                        CurrencyCode = MoneyTestUtility.ValidCurrencyCode[0].ToString(),
                        Amount = 0
                    }
                }
            };

            // Act
            var result = await _updateProductCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }
    }
}
