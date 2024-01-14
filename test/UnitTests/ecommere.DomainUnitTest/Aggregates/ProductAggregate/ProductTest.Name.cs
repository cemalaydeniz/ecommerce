using ecommerce.Domain.Aggregates.ProductAggregate.Events;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommere.DomainUnitTest.Aggregates.ProductAggregate.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.ProductAggregate
{
    public partial class ProductTest
    {
        public class NameTest
        {
            [Theory]
            [MemberData(nameof(ProductTestUtility.InvalidNames), MemberType = typeof(ProductTestUtility))]
            public void UpdateName_WhenNameIsInvalid_ShouldThrowException(string name)
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;

                // Act
                var result = Record.Exception(() =>
                {
                    product.UpdateName(name);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void UpdateName_WhenNameIsSame_ShouldReturnFalseAndNotAddDomainEvent()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;
                var name = product.Name;

                // Act
                var result = product.UpdateName(name);

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(ProductNameUpdated), product.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void UpdateName_WhenNameIsDifferent_ShouldReturnTrueAndUpdateNameAndAddDomainEvent()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;
                var name = product.Name + "a";

                // Act
                var result = product.UpdateName(name);

                // Assert
                Assert.True(result);
                Assert.Equal(name, product.Name);
                Assert.Contains(typeof(ProductNameUpdated), product.DomainEvents.Select(x => x.GetType()));
            }
        }
    }
}
