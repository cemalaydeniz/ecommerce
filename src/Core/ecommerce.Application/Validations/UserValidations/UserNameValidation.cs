using ecommerce.Application.Utilities.Constants;
using ecommerce.Domain.Aggregates.UserAggregate;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.UserValidations
{
    public class UserNameValidation : AbstractValidator<string>
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.UserValidation.NameRequired));
                return false;
            }

            return true;
        }

        public UserNameValidation()
        {
            RuleFor(un => un)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.UserValidation.NameRequired)
                .Length(User.NameMinLength, User.NameMaxLength)
                    .WithMessage(ConstantsUtility.UserValidation.NameLength_MinMax);
        }
    }
}
