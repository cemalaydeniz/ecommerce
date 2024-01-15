using ecommerce.Application.Utilities.Constants;
using ecommerce.Domain.Common.ValueObjects;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.PaymentValidations
{
    public class PaymentCurrencyCodeValidation : AbstractValidator<string>
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (string.IsNullOrEmpty(context.InstanceToValidate))
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.PaymentValidation.CurrencyCodeRequired));
                return false;
            }

            return true;
        }

        public PaymentCurrencyCodeValidation()
        {
            RuleFor(cc => cc)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.PaymentValidation.CurrencyCodeRequired)
                .MaximumLength(Money.CurrenyCodeMaxLength)
                    .WithMessage(ConstantsUtility.MoneyValidation.CurrencyCodeLength_Max);
        }
    }
}
