namespace ecommerce.Domain.Aggregates.ProductAggregate.Exceptions
{
    public class DuplicatedCurrencyException : Exception
    {
        public DuplicatedCurrencyException() : base() { }
        public DuplicatedCurrencyException(string message) : base(message) { }
    }
}
