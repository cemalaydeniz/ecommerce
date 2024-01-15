#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

using ecommerce.Application.Validations.ProductValidations.Nullable;
using FluentValidation;

namespace ecommerce.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandValidation()
        {
            RuleFor(x => x.NewName)
                .SetValidator(new ProductNameNullableValidation());

            RuleFor(x => x.NewDescription)
                .SetValidator(new ProductDescriptionNullableValidation());

            RuleFor(x => x.CurrencyCodesToRemove)
                .SetValidator(new ProductRemovePricesNullableValidation());

            RuleFor(x => x.PricesToUpdateOrAdd)
                .SetValidator(new ProductPricesNullableValidation());
        }
    }
}
