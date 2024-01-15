using ecommerce.Application.Utilities.Constants;
using ecommerce.Domain.Aggregates.ProductAggregate;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.ProductValidations
{
    public class ProductDescriptionValidation : AbstractValidator<string>
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.ProductValidation.DescriptionRequired));
                return false;
            }

            return true;
        }

        public ProductDescriptionValidation()
        {
            RuleFor(d => d)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.ProductValidation.DescriptionRequired)
                .Length(Product.DescriptionMinLength, Product.DescriptionMaxLength)
                    .WithMessage(ConstantsUtility.ProductValidation.DescriptionLength_MinMax);
        }
    }
}
