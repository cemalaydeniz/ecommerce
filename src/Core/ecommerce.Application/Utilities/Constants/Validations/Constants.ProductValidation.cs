namespace ecommerce.Application.Utilities.Constants
{
    public static partial class ConstantsUtility
    {
        public static class ProductValidation
        {
            public static readonly string NameRequired = "Name is required";
            public static readonly string DescriptionRequired = "Description is required";
            public static readonly string PricesRequired = "Prices is required";
            public static readonly string CurrencyCodeRemoveRequired = "Currency code is required to remove";

            public static readonly string NameLength_MinMax = $"Name must be between {Domain.Aggregates.ProductAggregate.Product.NameMinLength} and {Domain.Aggregates.ProductAggregate.Product.NameMaxLength} characters";
            public static readonly string DescriptionLength_MinMax = $"Description must be between {Domain.Aggregates.ProductAggregate.Product.DescriptionMinLength} and {Domain.Aggregates.ProductAggregate.Product.DescriptionMaxLength} characters";

            public static readonly string PriceNotFreeForAllCurrencies = "The product is free for one or more currencies while being paid for some. It can be either free or paid for all currencies";
        }
    }
}
