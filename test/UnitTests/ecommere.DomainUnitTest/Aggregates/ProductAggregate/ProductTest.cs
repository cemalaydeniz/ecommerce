using ecommerce.Domain.Aggregates.ProductAggregate.Events;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;

namespace ecommerce.DomainUnitTest.Aggregates.ProductAggregate
{
    public partial class ProductTest
    {
        [Theory]
        [MemberData(nameof(ProductTestUtility.ValidValues), MemberType = typeof(ProductTestUtility))]
        public void CreateProductEntity_WhenValuesAreValid_ShouldThrowNoExceptionAndAddDomainEvent(string name,
            List<Money> prices,
            string description)
        {
            // Arrange
            Product? product = null;

            // Act
            var result = Record.Exception(() =>
            {
                product = new Product(name, prices, description);
            });

            // Assert
            Assert.Null(result);
            Assert.Contains(typeof(ProductCreated), product!.DomainEvents.Select(x => x.GetType()));
        }

        [Theory]
        [MemberData(nameof(ProductTestUtility.InvalidValues), MemberType = typeof(ProductTestUtility))]
        public void CreateProductEntity_WhenValuesAreInvalid_ShouldThrowException(string name,
            List<Money> prices,
            string description)
        {
            // Act
            var result = Record.Exception(() =>
            {
                new Product(name, prices, description);
            });

            // Assert
            Assert.NotNull(result);
        }
    }
}
