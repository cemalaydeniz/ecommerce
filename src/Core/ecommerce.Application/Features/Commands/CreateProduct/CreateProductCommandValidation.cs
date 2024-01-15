using ecommerce.Application.Validations.ProductValidations;
using FluentValidation;

namespace ecommerce.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommandValidation : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidation()
        {
            RuleFor(x => x.Name)
                .SetValidator(new ProductNameValidation());

            RuleFor(x => x.Prices)
                .SetValidator(new ProductPricesValidation());

            RuleFor(x => x.Description)
                .SetValidator(new ProductDescriptionValidation());
        }
    }
}
