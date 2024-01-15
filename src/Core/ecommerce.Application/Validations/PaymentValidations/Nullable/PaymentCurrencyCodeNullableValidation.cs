using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.PaymentValidations.Nullable
{
    public class PaymentCurrencyCodeNullableValidation : PaymentCurrencyCodeValidation
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (string.IsNullOrEmpty(context.InstanceToValidate))
                return false;

            return true;
        }

        public PaymentCurrencyCodeNullableValidation() : base() { }
    }
}
