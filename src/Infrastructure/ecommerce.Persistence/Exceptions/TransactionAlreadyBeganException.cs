namespace ecommerce.Persistence.Exceptions
{
    public class TransactionAlreadyBeganException : Exception
    {
        public TransactionAlreadyBeganException() : base() { }
        public TransactionAlreadyBeganException(string message) : base(message) { }
    }
}
