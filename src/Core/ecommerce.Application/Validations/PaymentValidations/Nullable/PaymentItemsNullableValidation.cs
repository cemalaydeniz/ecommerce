using ecommerce.Application.Features.Queries.InitiatePayment;
using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.PaymentValidations.Nullable
{
    public class PaymentItemsNullableValidation : PaymentItemsValidation
    {
        protected override bool PreValidate(ValidationContext<List<InitiatePaymentQueryRequest.Item>> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null || context.InstanceToValidate.Count == 0)
                return false;

            return true;
        }

        public PaymentItemsNullableValidation() : base() { }
    }
}
