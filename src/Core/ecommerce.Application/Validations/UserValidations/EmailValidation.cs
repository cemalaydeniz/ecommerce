using ecommerce.Application.Utilities.Constants;
using ecommerce.Domain.Aggregates.UserAggregate;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.UserValidations
{
    public class EmailValidation : AbstractValidator<string>
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.UserValidation.EmailRequired));
                return false;
            }

            return true;
        }

        public EmailValidation()
        {
            RuleFor(e => e)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.UserValidation.EmailRequired)
                .MaximumLength(User.EmailMaxLength)
                    .WithMessage(ConstantsUtility.UserValidation.EmailLength_Max)
                .Matches(User.EmailRegex)
                    .WithMessage(ConstantsUtility.UserValidation.InvalidEmail);
        }
    }
}
