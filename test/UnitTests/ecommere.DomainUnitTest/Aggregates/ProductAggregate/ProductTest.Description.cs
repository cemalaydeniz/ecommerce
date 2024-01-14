using ecommerce.Domain.Aggregates.ProductAggregate.Events;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;

namespace ecommerce.DomainUnitTest.Aggregates.ProductAggregate
{
    public partial class ProductTest
    {
        public class DescriptionTest
        {
            [Theory]
            [MemberData(nameof(ProductTestUtility.InvalidDescriptions), MemberType = typeof(ProductTestUtility))]
            public void UpdateDescription_WhenDescriptionIsInvalid_ShouldThrowException(string description)
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;

                // Act
                var result = Record.Exception(() =>
                {
                    product.UpdateDescription(description);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void UpdateDescription_WhenNameIsSame_ShouldReturnFalseAndNotAddDomainEvent()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;
                var description = product.Description;

                // Act
                var result = product.UpdateDescription(description);

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(ProductDescriptionUpdated), product.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void UpdateName_WhenNameIsDifferent_ShouldReturnTrueAndUpdateNameAndAddDomainEvent()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;
                var description = product.Description + "a";

                // Act
                var result = product.UpdateDescription(description);

                // Assert
                Assert.True(result);
                Assert.Equal(description, product.Description);
                Assert.Contains(typeof(ProductDescriptionUpdated), product.DomainEvents.Select(x => x.GetType()));
            }
        }
    }
}
