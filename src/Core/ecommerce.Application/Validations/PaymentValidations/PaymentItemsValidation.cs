using ecommerce.Application.Features.Queries.InitiatePayment;
using ecommerce.Application.Utilities.Constants;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.PaymentValidations
{
    public class PaymentItemsValidation : AbstractValidator<List<InitiatePaymentQueryRequest.Item>>
    {
        protected override bool PreValidate(ValidationContext<List<InitiatePaymentQueryRequest.Item>> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null || context.InstanceToValidate.Count == 0)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.PaymentValidation.QuantityRequired));
                return false;
            }

            return true;
        }

        public PaymentItemsValidation()
        {
            RuleForEach(i => i)
                .SetValidator(new PaymentItemValidation());
        }
    }
}
