using ecommerce.Application.Features.Queries.InitiatePayment;
using ecommerce.Application.Utilities.Constants;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.PaymentValidations
{
    public class PaymentItemValidation : AbstractValidator<InitiatePaymentQueryRequest.Item>
    {
        protected override bool PreValidate(ValidationContext<InitiatePaymentQueryRequest.Item> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.PaymentValidation.ItemRequired));
                return false;
            }

            return true;
        }

        public PaymentItemValidation()
        {
            RuleFor(i => i.Quantity)
                .GreaterThan(0)
                    .WithMessage(ConstantsUtility.PaymentValidation.QuantityGreaterThanZero);
        }
    }
}
