namespace ecommerce.Domain.Common.Exceptions
{
    public class MoneyExceedException : Exception
    {
        public MoneyExceedException() : base() { }
        public MoneyExceedException(string message) : base(message) { }
    }
}
