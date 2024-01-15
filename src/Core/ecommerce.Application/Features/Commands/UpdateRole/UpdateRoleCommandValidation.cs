using ecommerce.Application.Validations.RoleValidations;
using FluentValidation;

namespace ecommerce.Application.Features.Commands.UpdateRole
{
    public class UpdateRoleCommandValidation : AbstractValidator<UpdateRoleCommandRequest>
    {
        public UpdateRoleCommandValidation()
        {
            RuleFor(x => x.NewName)
                .SetValidator(new RoleNameValidation());
        }
    }
}
