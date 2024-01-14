using ecommerce.Domain.Aggregates.ProductAggregate.Events;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommere.DomainUnitTest.Aggregates.ProductAggregate.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.ProductAggregate
{
    public partial class ProductTest
    {
        public class MakeFreeTest
        {
            [Fact]
            public void MakeProductFree_WhenProductIsNotFree_ShouldReturnTrueAndUpdateProductAndAddDomainEvent()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;

                // Act
                var result = product.MakeFree();

                // Assert
                Assert.True(result);
                Assert.True((bool)product.Prices.Any(m => m.Amount == 0));
                Assert.Contains(typeof(ProductBecameFree), product.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void MakeProductFree_WhenProductIsAlreadyFree_ShouldReturnFalseAndAndNotAddDomainEvent()
            {
                // Arrange
                Product freeProduct = ProductTestUtility.ValidFreeProduct;

                // Act
                var result = freeProduct.MakeFree();

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(ProductBecameFree), freeProduct.DomainEvents.Select(x => x.GetType()));
            }
        }
    }
}
