using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.PaymentValidations.Nullable
{
    public class PaymentHttpHeaderNullableValidation : AbstractValidator<string>
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(context.InstanceToValidate))
                return false;

            return true;
        }

        public PaymentHttpHeaderNullableValidation() : base() { }
    }
}
