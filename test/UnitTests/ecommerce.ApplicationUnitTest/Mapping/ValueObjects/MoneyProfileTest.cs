using ecommerce.Application.Models.ValueObjects;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.DomainUnitTest.Common.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Mapping.ValueObjects
{
    public class MoneyProfileTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _mapperFixture;

        public MoneyProfileTest(AutoMapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
        }

        [Fact]
        public void MapFromModelModelToModel_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new MoneyModel()
            {
                CurrencyCode = MoneyTestUtility.ValidCurrencyCode,
                Amount = MoneyTestUtility.ValidAmount
            };

            // Act
            var result = _mapperFixture.Mapper.Map<Money>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.CurrencyCode, result.CurrencyCode);
            Assert.Equal(model.Amount, result.Amount);
        }
    }
}
