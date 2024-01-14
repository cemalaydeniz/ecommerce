namespace ecommerce.Domain.Aggregates.OrderRepository.Exceptions
{
    public class DuplicatedOrderItemException : Exception
    {
        public DuplicatedOrderItemException() : base() { }
        public DuplicatedOrderItemException(string message) : base(message) { }
    }
}
