namespace ecommerce.Persistence.Exceptions
{
    public class TransactionNotStartedException : Exception
    {
        public TransactionNotStartedException() : base() { }
        public TransactionNotStartedException(string message) : base(message) { }
    }
}
