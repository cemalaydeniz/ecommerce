using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.ProductValidations.Nullable
{
    public class ProductDescriptionNullableValidation : ProductDescriptionValidation
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(context.InstanceToValidate))
                return false;

            return true;
        }

        public ProductDescriptionNullableValidation() : base() { }
    }
}
