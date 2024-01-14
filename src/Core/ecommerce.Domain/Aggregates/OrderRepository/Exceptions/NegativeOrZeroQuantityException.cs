namespace ecommerce.Domain.Aggregates.OrderRepository.Exceptions
{
    public class NegativeOrZeroQuantityException : Exception
    {
        public NegativeOrZeroQuantityException() : base() { }
        public NegativeOrZeroQuantityException(string message) : base(message) { }
    }
}
