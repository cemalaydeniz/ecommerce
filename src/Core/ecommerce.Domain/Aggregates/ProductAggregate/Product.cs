#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.Aggregates.ProductAggregate.Events;
using ecommerce.Domain.Aggregates.ProductAggregate.Exceptions;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.ProductAggregate
{
    public class Product : BaseEntity<Guid>, ISoftDelete
    {
        #region Properties
        public string Name { get; private set; }
        public List<Money> Prices { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        #endregion

        #region Behaviors
        private Product() { }

        /// <summary>
        /// Creates a new product locally
        /// </summary>
        /// <param name="name">The name of the product</param>
        /// <param name="price">The price of the product</param>
        /// <param name="description">The description of the product</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public Product(string name,
            List<Money> prices,
            string description)
        {
            ValidateName(name);
            ValidatePrices(prices);
            ValidateDescription(description);

            Id = Guid.NewGuid();
            Name = name;
            Description = description;

            if (prices.Any(m => m.Amount == 0))
            {
                Prices = new List<Money>
                {
                    Money.Zero
                };
            }
            else
            {
                Prices = prices;
            }

            base.AddDomainEvents(new ProductCreated(this));
        }

        /// <summary>
        /// Updates the name of the product
        /// </summary>
        /// <param name="newName">The new name to be set</param>
        /// <returns>TRUE if the new name is set successfully, FALSE if the new name is already the same as the old one</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        public bool UpdateName(string newName)
        {
            ValidateName(newName);

            if (Name == newName)
                return false;

            Name = newName;

            base.AddDomainEvents(new ProductNameUpdated(this, newName));
            return true;
        }

        /// <summary>
        /// Makes the product free of charge
        /// </summary>
        /// <returns>TRUE if the product is set as free, FALSE if the product is already free of charge</returns>
        public bool MakeFree()
        {
            if (Prices.Any(m => m.Amount == 0))
                return false;

            Prices = new List<Money>()
            {
                Money.Zero
            };

            base.AddDomainEvents(new ProductBecameFree(this));
            return true;
        }

        /// <summary>
        /// Adds a new currency/amount to the price list of the product
        /// </summary>
        /// <param name="newPrice">The new price to be added to the list</param>
        /// <returns>TRUE if the new price is added successfully, FALSE if the new price already exists in the price list</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ProductIsFreeException"></exception>
        /// <exception cref="ProductNotFreeForAllCurrenciesException"></exception>
        public bool AddPrice(Money newPrice)
        {
            ValidatePrice(newPrice);

            if (Prices.Any(p => p.CurrencyCode == newPrice.CurrencyCode))
                return false;

            Prices.Add(newPrice);

            base.AddDomainEvents(new ProductPriceAdded(this, newPrice));
            return true;
        }

        /// <summary>
        /// Updates one of the prices of the product
        /// </summary>
        /// <param name="priceToUpdate">The price to be updated</param>
        /// <returns>TRUE if the price is updated successfully, FALSE if the price is the same as the old one</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ProductIsFreeException"></exception>
        /// <exception cref="ProductNotFreeForAllCurrenciesException"></exception>
        /// <exception cref="PriceNotFoundException"></exception>
        public bool UpdatePrice(Money priceToUpdate)
        {
            ValidatePrice(priceToUpdate);

            Money? price = Prices.Find(p => p.CurrencyCode == priceToUpdate.CurrencyCode);
            if (price == null)
                throw new PriceNotFoundException($"No price with the currency code of {priceToUpdate.CurrencyCode} was found");

            if (price == priceToUpdate)
                return false;

            int index = Prices.IndexOf(price);
            Prices[index] = new Money(priceToUpdate.CurrencyCode, priceToUpdate.Amount);

            base.AddDomainEvents(new ProductPriceUpdated(this, priceToUpdate));
            return true;
        }

        /// <summary>
        /// Removes a price from the price list of the product
        /// </summary>
        /// <param name="currencyCode">The currency code of the price to be removed from the list</param>
        /// <returns>TRUE if the price is removed successfully, FALSE if the price does not exist in the address list</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ProductHasNoPriceException"></exception>
        public bool RemovePrice(string currencyCode)
        {
            ValidateCurrencyCode(currencyCode);

            Money? price = Prices.Find(p => p.CurrencyCode == currencyCode);
            if (price == null)
                return false;

            if (Prices.Count == 1)
                throw new ProductHasNoPriceException("There is already only 1 price defined for this product. It cannot be removed");

            Prices.Remove(price);

            base.AddDomainEvents(new ProductPriceRemoved(this, price));
            return true;
        }

        /// <summary>
        /// Updates the description of the product
        /// </summary>
        /// <param name="newDescription">The description to be set</param>
        /// <returns>TRUE if the new description is set successfully, FALSE if the new description is already the same as the old one</returns>
        public bool UpdateDescription(string newDescription)
        {
            ValidateDescription(newDescription);

            if (Description == newDescription)
                return false;

            Description = newDescription;

            base.AddDomainEvents(new ProductDescriptionUpdated(this, newDescription));
            return true;
        }

        /// <summary>
        /// Marks the product as deleted
        /// </summary>
        /// <returns>TRUE if the product is marked as deleted successfully, FALSE if the product is already marked as deleted</returns>
        public bool Delete()
        {
            if (IsDeleted)
                return false;

            IsDeleted = true;

            base.AddDomainEvents(new ProductSoftDeleted(this));
            return true;
        }
        #endregion

        #region Validations
        public static readonly int NameMinLength = 3;
        public static readonly int NameMaxLength = 100;
        public static readonly int DescriptionMinLength = 3;
        public static readonly int DescriptionMaxLength = 1000;

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (name.Length < NameMinLength || name.Length > NameMaxLength)
                throw new CharLengthOutofRangeException(nameof(name), NameMinLength, NameMaxLength);
        }

        private void ValidatePrices(List<Money> prices)
        {
            if (prices == null)
                throw new ArgumentNullException(nameof(prices));
            if (prices.Count == 0)
                throw new InvalidOperationException($"{nameof(prices)} is empty");
            if (prices.GroupBy(p => p.CurrencyCode).Any(x => x.Count() > 1))
                throw new DuplicatedCurrencyException();

            if (prices.Any(m => m.Amount == 0))
            {
                foreach (var price in prices)
                {
                    if (price.Amount != 0)
                        throw new ProductNotFreeForAllCurrenciesException("The product is free for one or more currencies while being paid for some. It can be either free or paid for all currencies");
                }
            }
        }

        private void ValidatePrice(Money price)
        {
            if (price == null)
                throw new ArgumentNullException(nameof(price));

            bool isFree = Prices.Any(m => m.Amount == 0);
            if (isFree)
                throw new ProductIsFreeException("The product is already free");
            if (price.Amount == 0)
                throw new ProductNotFreeForAllCurrenciesException("The product is already paid");
        }

        private void ValidateCurrencyCode(string currencyCode)
        {
            if (string.IsNullOrEmpty(currencyCode))
                throw new ArgumentNullException(nameof(currencyCode));
        }

        private void ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(nameof(description));
            if (description.Length < DescriptionMinLength || description.Length > DescriptionMaxLength)
                throw new CharLengthOutofRangeException(nameof(description), DescriptionMinLength, DescriptionMaxLength);
        }
        #endregion
    }
}
