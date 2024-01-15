using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.ProductValidations.Nullable
{
    public class ProductNameNullableValidation : ProductNameValidation
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(context.InstanceToValidate))
                return false;

            return true;
        }

        public ProductNameNullableValidation() : base() { }
    }
}
