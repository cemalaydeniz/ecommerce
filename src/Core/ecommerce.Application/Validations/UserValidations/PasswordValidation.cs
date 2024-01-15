using ecommerce.Application.Utilities.Constants;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.UserValidations
{
    public class PasswordValidation : AbstractValidator<string>
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.UserValidation.PasswordRequired));
                return false;
            }

            return true;
        }

        public PasswordValidation()
        {
            RuleFor(p => p)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.UserValidation.PasswordRequired)
                .Length(ConstantsUtility.UserValidation.PasswordMinLength, ConstantsUtility.UserValidation.PasswordMaxLength)
                    .WithMessage(ConstantsUtility.UserValidation.PasswordLength_MinMax)
                .Matches(ConstantsUtility.UserValidation.PasswordRegex)
                    .WithMessage(ConstantsUtility.UserValidation.InvalidPassword);
        }
    }
}
