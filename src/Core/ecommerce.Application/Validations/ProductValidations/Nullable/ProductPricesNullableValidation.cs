using ecommerce.Application.Models.ValueObjects;
using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.ProductValidations.Nullable
{
    public class ProductPricesNullableValidation : ProductPricesValidation
    {
        protected override bool PreValidate(ValidationContext<List<MoneyModel>> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null || context.InstanceToValidate.Count == 0)
                return false;

            return true;
        }

        public ProductPricesNullableValidation() : base() { }
    }
}
