namespace ecommerce.Domain.Common.Exceptions
{
    public class NegativeMoneyException : Exception
    {
        public NegativeMoneyException() : base() { }
        public NegativeMoneyException(string message) : base(message) { }
    }
}
