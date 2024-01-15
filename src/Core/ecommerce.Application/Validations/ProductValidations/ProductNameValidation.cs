using ecommerce.Application.Utilities.Constants;
using ecommerce.Domain.Aggregates.ProductAggregate;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.ProductValidations
{
    public class ProductNameValidation : AbstractValidator<string>
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.ProductValidation.NameRequired));
                return false;
            }

            return true;
        }

        public ProductNameValidation()
        {
            RuleFor(pn => pn)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.ProductValidation.NameRequired)
                .Length(Product.NameMinLength, Product.NameMaxLength)
                    .WithMessage(ConstantsUtility.ProductValidation.NameLength_MinMax);
        }
    }
}
