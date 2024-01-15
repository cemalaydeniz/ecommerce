using ecommerce.Application.Utilities.Constants;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.PaymentValidations
{
    public class PaymentHttpHeaderValidation : AbstractValidator<string>
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(context.InstanceToValidate))
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.PaymentValidation.HttpHeaderRequired));
                return false;
            }

            return true;
        }

        public PaymentHttpHeaderValidation()
        {
            RuleFor(b => b)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.PaymentValidation.HttpHeaderRequired);
        }
    }
}
