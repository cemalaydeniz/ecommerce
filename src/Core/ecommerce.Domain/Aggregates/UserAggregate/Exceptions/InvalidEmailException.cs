namespace ecommerce.Domain.Aggregates.UserAggregate.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base() { }
        public InvalidEmailException(string message) : base(message) { }
    }
}
