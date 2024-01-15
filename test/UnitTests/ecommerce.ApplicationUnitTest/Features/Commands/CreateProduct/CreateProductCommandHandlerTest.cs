using ecommerce.Application.Features.Commands.CreateProduct;
using ecommerce.Application.Models.ValueObjects;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.CreateProduct
{
    public class CreateProductCommandHandlerTest : IClassFixture<UnitofWorkFixture>, IClassFixture<AutoMapperFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;
        private readonly AutoMapperFixture _autoMapperFixture;

        private CreateProductCommandHandler _createProductCommandHandler;

        public CreateProductCommandHandlerTest(UnitofWorkFixture unitofWorkFixture,
            AutoMapperFixture autoMapperFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;
            _autoMapperFixture = autoMapperFixture;

            _createProductCommandHandler = new CreateProductCommandHandler(_unitofWorkFixture.UnitofWork, _autoMapperFixture.Mapper);
        }

        [Fact]
        public async Task CreateProductCommandHandler_WhenValuesAreValid_ShouldCreateNewProduct()
        {
            // Arrange
            var request = new CreateProductCommandRequest()
            {
                Name = ProductTestUtility.ValidName,
                Prices = _autoMapperFixture.Mapper.Map<List<MoneyModel>>(ProductTestUtility.ValidPrices),
                Description = ProductTestUtility.ValidDescription
            };

            // Act
            var result = await _createProductCommandHandler.Handle(request, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(await _unitofWorkFixture.UnitofWork.ProductRepository.GetByIdAsync(result.Response!.ProductId, false));
        }
    }
}
