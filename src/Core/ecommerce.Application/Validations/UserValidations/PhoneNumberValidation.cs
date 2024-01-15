using ecommerce.Application.Utilities.Constants;
using ecommerce.Domain.Aggregates.UserAggregate;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.UserValidations
{
    public class PhoneNumberValidation : AbstractValidator<string>
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.UserValidation.PhoneNumberRequired));
                return false;
            }

            return true;
        }

        public PhoneNumberValidation()
        {
            RuleFor(pn => pn)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.UserValidation.PhoneNumberRequired)
                .MaximumLength(User.PhoneNumberMaxLength)
                    .WithMessage(ConstantsUtility.UserValidation.PhoneNumberLength_Max);
        }
    }
}
