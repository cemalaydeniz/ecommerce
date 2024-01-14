namespace ecommerce.Domain.Aggregates.UserAggregate.Exceptions
{
    public class TokenExpiresInPastException : Exception
    {
        public TokenExpiresInPastException() : base() { }
        public TokenExpiresInPastException(string message) : base(message) { }
    }
}
