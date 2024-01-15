using ecommerce.Application.Models.ValueObjects;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Domain.Common.ValueObjects;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.ValueObjectValidations
{
    public class MoneyValidation : AbstractValidator<MoneyModel>
    {
        protected override bool PreValidate(ValidationContext<MoneyModel> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.MoneyValidation.MoneyRequired));
                return false;
            }

            return true;
        }

        public MoneyValidation()
        {
            RuleFor(m => m.CurrencyCode)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(ConstantsUtility.MoneyValidation.CurrencyCodeRequired)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.MoneyValidation.CurrencyCodeRequired)
                .MaximumLength(Money.CurrenyCodeMaxLength)
                    .WithMessage(ConstantsUtility.MoneyValidation.CurrencyCodeLength_Max);

            RuleFor(m => m.Amount)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(0)
                    .WithMessage(ConstantsUtility.MoneyValidation.AmountCannotBeNegative)
                .Custom((value, context) =>
                {
                    decimal integerPart = Math.Floor(value);
                    if (integerPart.ToString().Length > Money.AmountMaxDigitBeforeComma)
                    {
                        context.AddFailure(ConstantsUtility.MoneyValidation.AmountMaxDigitBeforeComma);
                        return;
                    }

                    decimal decimalPart = value - integerPart;
                    if (decimalPart.ToString().Length - 2 > Money.AmountMaxDigitAfterComma)
                    {
                        context.AddFailure(ConstantsUtility.MoneyValidation.AmountMaxDigitAfterComma);
                    }
                });
        }
    }
}
