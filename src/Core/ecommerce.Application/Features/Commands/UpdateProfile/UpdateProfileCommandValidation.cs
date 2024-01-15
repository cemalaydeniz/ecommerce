#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

using ecommerce.Application.Validations.UserValidations.Nullable;
using ecommerce.Application.Validations.ValueObjectValidations.Nullable;
using FluentValidation;

namespace ecommerce.Application.Features.Commands.UpdateProfile
{
    public class UpdateProfileCommandValidation : AbstractValidator<UpdateProfileCommandRequest>
    {
        public UpdateProfileCommandValidation()
        {
            RuleFor(x => x.NewName)
                .SetValidator(new UserNameNullableValidation());

            RuleFor(x => x.NewPhoneNumber)
                .SetValidator(new PhoneNumberNullableValidation());

            RuleFor(x => x.TitleofAddressToUpdate)
                .SetValidator(new UserAddressTitleNullableValidation());

            RuleFor(x => x.UserAddress)
                .SetValidator(new UserAddressNullableValidation());
        }
    }
}
