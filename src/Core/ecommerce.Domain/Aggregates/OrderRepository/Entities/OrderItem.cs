using ecommerce.Domain.Aggregates.OrderRepository.Exceptions;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderRepository.Entities
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
