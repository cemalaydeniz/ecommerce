namespace ecommerce.Application.Utilities.Constants
{
    public static partial class ConstantsUtility
    {
        public static class Payment
        {
            public const string UserNameRequired = "You must provide a name to make a purchase";
            public const string AddressRequired = "You must provide an address to make a purchase";

            public const string RepeatedProduct = "There is more than one same product";

            public const string ProductsNotFound = "One or more products were not found";
            public const string ProductsCurrencyCodeDifferent = "One or more products are not available due to the currency difference";

            public const string ItemDataSeperator = ",";
        }
    }
}
