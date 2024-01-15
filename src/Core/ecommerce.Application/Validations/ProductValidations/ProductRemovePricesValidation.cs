using ecommerce.Application.Utilities.Constants;
using ecommerce.Domain.Common.ValueObjects;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.ProductValidations
{
    public class ProductRemovePricesValidation : AbstractValidator<List<string>>
    {
        protected override bool PreValidate(ValidationContext<List<string>> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null || context.InstanceToValidate.Count() == 0)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.ProductValidation.CurrencyCodeRemoveRequired));
                return false;
            }

            return true;
        }

        public ProductRemovePricesValidation()
        {
            RuleForEach(cc => cc)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(ConstantsUtility.ProductValidation.CurrencyCodeRemoveRequired)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.ProductValidation.CurrencyCodeRemoveRequired)
                .MaximumLength(Money.CurrenyCodeMaxLength)
                    .WithMessage(ConstantsUtility.MoneyValidation.CurrencyCodeLength_Max);
        }
    }
}
