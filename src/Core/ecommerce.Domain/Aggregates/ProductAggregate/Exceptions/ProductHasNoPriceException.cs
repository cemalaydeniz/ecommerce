namespace ecommerce.Domain.Aggregates.ProductAggregate.Exceptions
{
    public class ProductHasNoPriceException : Exception
    {
        public ProductHasNoPriceException() : base() { }
        public ProductHasNoPriceException(string message) : base(message) { }
    }
}
