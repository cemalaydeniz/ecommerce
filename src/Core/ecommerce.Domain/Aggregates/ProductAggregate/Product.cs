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
        /// <summary>
        /// Marks the product as deleted
        /// </summary>
        /// <returns>TRUE if the product is marked as deleted successfully, FALSE if the product is already marked as deleted</returns>
        public bool Delete()
        {
            if (IsDeleted)
                return false;

            IsDeleted = true;

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
