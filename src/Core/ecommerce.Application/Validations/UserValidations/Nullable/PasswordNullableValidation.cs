using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.UserValidations.Nullable
{
    public class PasswordNullableValidation : PasswordValidation
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(context.InstanceToValidate))
                return false;

            return true;
        }

        public PasswordNullableValidation() : base() { }
    }
}
