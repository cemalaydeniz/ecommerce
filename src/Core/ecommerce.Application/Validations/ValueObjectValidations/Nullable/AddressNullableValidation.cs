using ecommerce.Application.Models.ValueObjects;
using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.ValueObjectValidations.Nullable
{
    public class AddressNullableValidation : AddressValidation
    {
        protected override bool PreValidate(ValidationContext<AddressModel> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
                return false;

            return true;
        }

        public AddressNullableValidation() : base() { }
    }
}
