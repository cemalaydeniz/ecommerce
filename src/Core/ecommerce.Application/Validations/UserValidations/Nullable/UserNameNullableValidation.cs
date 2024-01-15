using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.UserValidations.Nullable
{
    public class UserNameNullableValidation : UserNameValidation
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(context.InstanceToValidate))
                return false;

            return true;
        }

        public UserNameNullableValidation() : base() { }
    }
}
