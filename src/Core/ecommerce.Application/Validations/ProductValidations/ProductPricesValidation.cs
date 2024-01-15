using ecommerce.Application.Models.ValueObjects;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.ValueObjectValidations;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.ProductValidations
{
    public class ProductPricesValidation : AbstractValidator<List<MoneyModel>>
    {
        protected override bool PreValidate(ValidationContext<List<MoneyModel>> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null || context.InstanceToValidate.Count == 0)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.ProductValidation.PricesRequired));
                return false;
            }

            return true;
        }

        public ProductPricesValidation()
        {
            RuleFor(list => list)
                .Must(l =>
                {
                    if (l.GroupBy(m => m.CurrencyCode).Any(x => x.Count() > 1))
                        return false;

                    if (l.Any(m => m.Amount == 0))
                    {
                        foreach (var price in l)
                        {
                            if (price.Amount != 0)
                                return false;
                        }
                    }
                    return true;
                })
                .WithMessage(ConstantsUtility.ProductValidation.PriceNotFreeForAllCurrencies);

            RuleForEach(list => list)
                .SetValidator(new MoneyValidation());
        }
    }
}
