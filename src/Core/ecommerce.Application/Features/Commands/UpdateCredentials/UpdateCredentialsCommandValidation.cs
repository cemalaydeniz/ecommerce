#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

using ecommerce.Application.Validations.UserValidations.Nullable;
using FluentValidation;

namespace ecommerce.Application.Features.Commands.UpdateCredentials
{
    public class UpdateCredentialsCommandValidation : AbstractValidator<UpdateCredentialsCommandRequest>
    {
        public UpdateCredentialsCommandValidation()
        {
            RuleFor(x => x.NewEmail)
                .SetValidator(new EmailNullableValidation());

            RuleFor(x => x.NewPassword)
                .SetValidator(new PasswordNullableValidation());
        }
    }
}
