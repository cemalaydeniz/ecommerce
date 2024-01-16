namespace ecommerce.Domain.Aggregates.OrderAggregate.Exceptions
{
    public class DuplicatedOrderItemException : Exception
    {
        public DuplicatedOrderItemException() : base() { }
        public DuplicatedOrderItemException(string message) : base(message) { }
    }
}
