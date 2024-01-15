namespace ecommerce.Application.Utilities.Constants
{
    public static partial class ConstantsUtility
    {
        public static class PaymentValidation
        {
            public static readonly string CurrencyCodeRequired = "Currency Code is required";
            public static readonly string QuantityRequired = "Quantity is required";
            public static readonly string ItemRequired = "Item is required";
            public static readonly string HttpHeaderRequired = "Header is required";
            public static readonly string HttpBodyRequired = "Body is required";

            public static readonly string QuantityGreaterThanZero = "Quantity must be greater than 0";
        }
    }
}
