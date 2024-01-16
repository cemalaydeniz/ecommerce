namespace ecommerce.Domain.Aggregates.OrderAggregate.Exceptions
{
    public class NegativeOrZeroQuantityException : Exception
    {
        public NegativeOrZeroQuantityException() : base() { }
        public NegativeOrZeroQuantityException(string message) : base(message) { }
    }
}
