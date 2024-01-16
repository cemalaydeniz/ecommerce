#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.Aggregates.OrderAggregate.Exceptions;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderAggregate.Entities
{
    public class OrderItem : BaseEntity<Guid>
    {
        #region Properties
        public string ProductName { get; private set; }
        public Money UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        // Navigations
        public Guid ProductId { get; private set; }
        #endregion

        #region Behaviors
        private OrderItem() { }

        /// <summary>
        /// Creates an item for an order locally
        /// </summary>
        /// <param name="productId">The Id of the product that this item is related to</param>
        /// <param name="productName">The name of the item</param>
        /// <param name="unitPrice">The unit price of the item</param>
        /// <param name="quantity">The quantity of the item</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        /// <exception cref="NegativeOrZeroQuantityException"></exception>
        public OrderItem(Guid productId,
            string productName,
            Money unitPrice,
            int quantity)
        {
            ValidateProductName(productName);
            ValidateQuantity(quantity);

            Id = Guid.NewGuid();
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;

            ProductId = productId;
        }
        #endregion

        #region Validations
        private void ValidateProductName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentNullException(nameof(productName));
            if (productName.Length < Product.NameMinLength || productName.Length > Product.NameMaxLength)
                throw new CharLengthOutofRangeException(nameof(productName), Product.NameMinLength, Product.NameMaxLength);
        }

        private void ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new NegativeOrZeroQuantityException($"The given quantity is {quantity}");
        }
        #endregion
    }
}
