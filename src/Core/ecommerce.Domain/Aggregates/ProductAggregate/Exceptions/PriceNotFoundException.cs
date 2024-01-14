namespace ecommerce.Domain.Aggregates.ProductAggregate.Exceptions
{
    public class PriceNotFoundException : Exception
    {
        public PriceNotFoundException() : base() { }
        public PriceNotFoundException(string message) : base(message) { }
    }
}
