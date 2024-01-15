using ecommerce.Application.Validations.PaymentValidations;
using FluentValidation;

namespace ecommerce.Application.Features.Queries.InitiatePayment
{
    public class InitiatePaymentQueryValidation : AbstractValidator<InitiatePaymentQueryRequest>
    {
        public InitiatePaymentQueryValidation()
        {
            RuleFor(x => x.CurrencyCode)
                .SetValidator(new PaymentCurrencyCodeValidation());

            RuleFor(x => x.Items)
                .SetValidator(new PaymentItemsValidation());
        }
    }
}
