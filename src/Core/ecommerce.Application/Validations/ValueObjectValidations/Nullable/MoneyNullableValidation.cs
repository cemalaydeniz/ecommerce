using ecommerce.Application.Models.ValueObjects;
using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.ValueObjectValidations.Nullable
{
    public class MoneyNullableValidation : MoneyValidation
    {
        protected override bool PreValidate(ValidationContext<MoneyModel> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
                return false;

            return true;
        }

        public MoneyNullableValidation() : base() { }
    }
}
