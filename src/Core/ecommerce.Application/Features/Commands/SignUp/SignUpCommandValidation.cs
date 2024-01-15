#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

using ecommerce.Application.Validations.UserValidations;
using ecommerce.Application.Validations.UserValidations.Nullable;
using ecommerce.Application.Validations.ValueObjectValidations.Nullable;
using FluentValidation;

namespace ecommerce.Application.Features.Commands.SignUp
{
    public class SignUpCommandValidation : AbstractValidator<SignUpCommandRequest>
    {
        public SignUpCommandValidation()
        {
            RuleFor(x => x.Email)
                .SetValidator(new EmailValidation());

            RuleFor(x => x.Password)
                .SetValidator(new PasswordValidation());

            RuleFor(x => x.Name)
                .SetValidator(new UserNameNullableValidation());

            RuleFor(x => x.PhoneNumber)
                .SetValidator(new PhoneNumberNullableValidation());

            RuleFor(x => x.Address)
                .SetValidator(new AddressNullableValidation());
        }
    }
}
