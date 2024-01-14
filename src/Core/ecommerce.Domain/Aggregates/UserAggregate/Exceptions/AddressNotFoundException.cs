namespace ecommerce.Domain.Aggregates.UserAggregate.Exceptions
{
    public class AddressNotFoundException : Exception
    {
        public AddressNotFoundException() : base() { }
        public AddressNotFoundException(string message) : base(message) { }
    }
}
