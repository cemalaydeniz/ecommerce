namespace ecommerce.Domain.Aggregates.ProductAggregate.Exceptions
{
    public class ProductNotFreeForAllCurrenciesException : Exception
    {
        public ProductNotFreeForAllCurrenciesException() : base() { }
        public ProductNotFreeForAllCurrenciesException(string message) : base(message) { }
    }
}
