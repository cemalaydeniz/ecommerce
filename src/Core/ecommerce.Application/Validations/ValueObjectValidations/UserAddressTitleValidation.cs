using ecommerce.Application.Utilities.Constants;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.ValueObjectValidations
{
    public class UserAddressTitleValidation : AbstractValidator<string>
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.AddressValidation.AddressTitleRequired));
                return false;
            }

            return true;
        }

        public UserAddressTitleValidation()
        {
            RuleFor(pn => pn)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.AddressValidation.AddressTitleRequired)
                .MaximumLength(UserAddress.TitleMaxLength)
                    .WithMessage(ConstantsUtility.AddressValidation.AddressTitleLength_Max);
        }
    }
}
