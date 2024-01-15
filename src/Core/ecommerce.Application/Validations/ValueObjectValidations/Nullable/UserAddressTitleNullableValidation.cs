using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.ValueObjectValidations.Nullable
{
    public class UserAddressTitleNullableValidation : UserAddressTitleValidation
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(context.InstanceToValidate))
                return false;

            return true;
        }

        public UserAddressTitleNullableValidation() : base() { }
    }
}
