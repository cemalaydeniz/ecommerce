using ecommerce.Application.Utilities.Constants;
using ecommerce.Domain.Aggregates.RoleAggregate;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.RoleValidations
{
    public class RoleNameValidation : AbstractValidator<string>
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.RoleValidation.NameRequired));
                return false;
            }

            return true;
        }

        public RoleNameValidation()
        {
            RuleFor(rn => rn)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.RoleValidation.NameRequired)
                .Length(Role.NameMinLength, Role.NameMaxLength)
                    .WithMessage(ConstantsUtility.RoleValidation.NameLength_MinMax);
        }
    }
}
