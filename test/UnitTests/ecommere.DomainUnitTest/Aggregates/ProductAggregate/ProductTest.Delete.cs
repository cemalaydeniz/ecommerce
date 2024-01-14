using ecommerce.Domain.Aggregates.ProductAggregate.Events;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommere.DomainUnitTest.Aggregates.ProductAggregate.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.ProductAggregate
{
    public partial class ProductTest
    {
        public class DeleteTest
        {
            [Fact]
            public void DeleteProduct_WhenProductIsAlreadyDeleted_ShouldReturnFalseAndNotAddDomainEvent()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;
                product.Delete();
                product.ClearDomainEvents();

                // Act
                var result = product.Delete();

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(ProductSoftDeleted), product.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void DeleteProduct_WhenProductIsNotDeleted_ShouldReturnTrueAndSetFlagAndDoesNotChangeAnythingAndAddDomainEvent()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;

                // Act
                var result = product.Delete();

                // Assert
                Assert.True(result);
                Assert.True(product.IsDeleted);
                Assert.NotNull(product.Name);
                Assert.NotEmpty(product.Prices);
                Assert.NotNull(product.Description);
                Assert.Contains(typeof(ProductSoftDeleted), product.DomainEvents.Select(x => x.GetType()));
            }
        }
    }
}
