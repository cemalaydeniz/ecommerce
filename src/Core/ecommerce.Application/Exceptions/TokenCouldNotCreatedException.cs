namespace ecommerce.Application.Exceptions
{
    public class TokenCouldNotCreatedException : Exception
    {
        public TokenCouldNotCreatedException() : base() { }
        public TokenCouldNotCreatedException(string message) : base(message) { }
    }
}
