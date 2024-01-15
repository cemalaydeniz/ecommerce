using ecommerce.Application.Models.ValueObjects;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.ValueObjectValidations
{
    public class UserAddressValidation : AbstractValidator<UserAddressModel>
    {
        protected override bool PreValidate(ValidationContext<UserAddressModel> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.AddressValidation.AddressRequired));
                return false;
            }

            return true;
        }

        public UserAddressValidation()
        {
            RuleFor(a => a.Title)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(ConstantsUtility.AddressValidation.AddressTitleRequired)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.AddressValidation.AddressTitleRequired)
                .MaximumLength(UserAddress.TitleMaxLength)
                    .WithMessage(ConstantsUtility.AddressValidation.AddressTitleLength_Max);

            RuleFor(a => a.Address)
                .SetValidator(new AddressValidation());
        }
    }
}
