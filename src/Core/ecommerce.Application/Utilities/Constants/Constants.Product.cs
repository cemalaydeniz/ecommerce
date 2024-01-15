namespace ecommerce.Application.Utilities.Constants
{
    public static partial class ConstantsUtility
    {
        public static class Product
        {
            public const string ProductNotFound = "The product was not found";
            public const string ProductMustHavePrice = "The product must have at least 1 price";
            public const string ProductIsFree = "The product is already free. The price cannot be changed";
            public const string ProductIsPaid = "The product is already paid. The price cannot be 0";
        }
    }
}
