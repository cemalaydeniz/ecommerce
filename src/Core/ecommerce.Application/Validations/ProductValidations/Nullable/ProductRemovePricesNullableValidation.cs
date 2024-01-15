using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.ProductValidations.Nullable
{
    public class ProductRemovePricesNullableValidation : ProductRemovePricesValidation
    {
        protected override bool PreValidate(ValidationContext<List<string>> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null || context.InstanceToValidate.Count() == 0)
                return false;

            return true;
        }

        public ProductRemovePricesNullableValidation() : base() { }
    }
}
