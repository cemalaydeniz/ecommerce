using ecommerce.Application.Features.Queries.InitiatePayment;
using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.PaymentValidations.Nullable
{
    public class PaymentItemNullableValidation : PaymentItemValidation
    {
        protected override bool PreValidate(ValidationContext<InitiatePaymentQueryRequest.Item> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
                return false;

            return true;
        }

        public PaymentItemNullableValidation() : base() { }
    }
}
