namespace ecommerce.Domain.Aggregates.ProductAggregate.Exceptions
{
    public class ProductIsFreeException : Exception
    {
        public ProductIsFreeException() : base() { }
        public ProductIsFreeException(string message) : base(message) { }
    }
}
