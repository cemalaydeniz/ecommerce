namespace ecommerce.Domain.Aggregates.UserAggregate.Exceptions
{
    public class DuplicatedAddressTitleException : Exception
    {
        public DuplicatedAddressTitleException() : base() { }
        public DuplicatedAddressTitleException(string message) : base(message) { }
    }
}
