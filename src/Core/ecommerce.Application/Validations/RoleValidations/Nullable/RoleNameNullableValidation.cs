using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.RoleValidations.Nullable
{
    public class RoleNameNullableValidation : RoleNameValidation
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(context.InstanceToValidate))
                return false;

            return true;
        }

        public RoleNameNullableValidation() : base() { }
    }
}
