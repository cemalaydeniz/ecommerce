using ecommerce.Application.Validations.UserValidations;
using FluentValidation;

namespace ecommerce.Application.Features.Commands.SignIn
{
    public class SignInCommandValidation : AbstractValidator<SignInCommandRequest>
    {
        public SignInCommandValidation()
        {
            RuleFor(x => x.Email)
                .SetValidator(new EmailValidation());

            RuleFor(x => x.Password)
                .SetValidator(new PasswordValidation());
        }
    }
}
