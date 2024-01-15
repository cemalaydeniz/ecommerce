using ecommerce.Application.Validations.PaymentValidations;
using FluentValidation;

namespace ecommerce.Application.Features.Commands.PaymentCheck
{
    public class PaymentCheckCommandValidation : AbstractValidator<PaymentCheckCommandRequest>
    {
        public PaymentCheckCommandValidation()
        {
            RuleFor(x => x.Header)
                .SetValidator(new PaymentHttpHeaderValidation());

            RuleFor(x => x.Body)
                .SetValidator(new PaymentHttpBodyValidation());
        }
    }
}
