using ecommerce.Application.Validations.RoleValidations;
using FluentValidation;

namespace ecommerce.Application.Features.Commands.CreateRole
{
    public class CreateRoleCommandValidation : AbstractValidator<CreateRoleCommandRequest>
    {
        public CreateRoleCommandValidation()
        {
            RuleFor(x => x.Name)
                .SetValidator(new RoleNameValidation());
        }
    }
}
