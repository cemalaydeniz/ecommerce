using ecommerce.Domain.Aggregates.ProductAggregate.Events;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;

namespace ecommerce.DomainUnitTest.Aggregates.ProductAggregate
{
    public partial class ProductTest
    {
        public class PriceTest
        {
            #region Add Price
            [Fact]
            public void AddPrice_WhenPriceIsNull_ShouldThrowException()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;

                // Act
                var result = Record.Exception(() =>
                {
                    product.AddPrice(null!);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void AddPrice_WhenPriceIsFreeAndProductNot_ShouldThrowException()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;
                Money price = Money.Zero;

                // Act
                var result = Record.Exception(() =>
                {
                    product.AddPrice(price);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void AddPrice_WhenProductIsFree_ShouldThrowException()
            {
                // Arrange
                Product freeProduct = ProductTestUtility.ValidFreeProduct;
                var price = Money.Zero;

                // Act
                var result = Record.Exception(() =>
                {
                    freeProduct.AddPrice(price);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void AddPrice_WhenCurrencyCodeIsSame_ShouldReturnFalseAndNotAddDomainEvent()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;
                var price = product.Prices[0];

                // Act
                var result = product.AddPrice(price);

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(ProductPriceAdded), product.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void AddPrice_WhenCurrencyCodeIsDifferent_ShouldReturnTrueAndAddToListAndAddDomainEvent()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;
                var price = new Money("AAA", 1);

                // Act
                var result = product.AddPrice(price);

                // Assert
                Assert.True(result);
                Assert.Contains(price, product.Prices);
                Assert.Contains(typeof(ProductPriceAdded), product.DomainEvents.Select(x => x.GetType()));
            }
            #endregion

            #region Update Price
            [Fact]
            public void UpdatePrice_WhenPriceIsNull_ShouldThrowException()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;

                // Act
                var result = Record.Exception(() =>
                {
                    product.UpdatePrice(null!);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void UpdatePrice_WhenPriceIsFreeAndProductNot_ShouldThrowException()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;
                var price = new Money(product.Prices[0].CurrencyCode, 0);

                // Act
                var result = Record.Exception(() =>
                {
                    product.UpdatePrice(price);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void UpdatePrice_WhenProductIsFree_ShouldThrowException()
            {
                // Arrange
                Product freeProduct = ProductTestUtility.ValidFreeProduct;
                var price = new Money("AAA", 1);

                // Act
                var result = Record.Exception(() =>
                {
                    freeProduct.UpdatePrice(price);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void UpdatePrice_WhenCurrencyCodeIsNotFound_ShouldThrowException()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;
                var price = new Money(product.Prices[0].CurrencyCode[0].ToString(), 1);

                // Act
                var result = Record.Exception(() =>
                {
                    product.UpdatePrice(price);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void UpdatePrice_WhenPriceIsSame_ShouldReturnFalseAndNotAddAsNewPriceAndNotAddDomainEvent()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;
                var price = product.Prices[0];

                // Act
                var result = product.UpdatePrice(price);

                // Assert
                Assert.False(result);
                Assert.False((bool)(product.Prices.GroupBy(x => x.CurrencyCode).Any(x => x.Count() > 1)));
                Assert.DoesNotContain(typeof(ProductPriceUpdated), product.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void UpdatePrice_WhenPriceIsDifferent_ShouldReturnTrueAndUpdatePriceAndAddDomainEvent()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;
                var price = new Money(product.Prices[0].CurrencyCode, 1);

                // Act
                var result = product.UpdatePrice(price);

                // Assert
                Assert.True(result);
                Assert.Equal(price, product.Prices[0]);
                Assert.Contains(typeof(ProductPriceUpdated), product.DomainEvents.Select(x => x.GetType()));
            }
            #endregion

            #region Delete Price
            [Fact]
            public void RemovePrice_WhenPriceIsNull_ShouldThrowException()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;

                // Act
                var result = Record.Exception(() =>
                {
                    product.RemovePrice(null!);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void RemovePrice_WhenCurrencyCodeNotFound_ShouldReturnFalseAndNotAddDomainEvent()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;

                // Act
                var result = product.RemovePrice(product.Prices[0].CurrencyCode[0].ToString());

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(ProductPriceRemoved), product.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void RemovePrice_WhenThereIsOnlyOnePrice_ShouldThrowException()
            {
                // Arrange
                Product freeProduct = ProductTestUtility.ValidFreeProduct;

                // Act
                var result = Record.Exception(() =>
                {
                    freeProduct.RemovePrice(freeProduct.Prices[0].CurrencyCode);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void RemovePrice_WhenCurrencyCodeExistsInList_ShouldReturnTrueAndRemoveFromListAndAddDomainEvent()
            {
                // Arrange
                Product product = ProductTestUtility.ValidProduct;
                var currencyCode = product.Prices[0].CurrencyCode;

                // Act
                var result = product.RemovePrice(currencyCode);

                // Assert
                Assert.True(result);
                Assert.False((bool)product.Prices.Any(m => m.CurrencyCode == currencyCode));
                Assert.Contains(typeof(ProductPriceRemoved), product.DomainEvents.Select(x => x.GetType()));
            }
            #endregion
        }
    }
}
