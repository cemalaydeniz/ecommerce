using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.UpdateRole
{
    public class UpdateRoleCommandRequest : IRequest<ValidationBehaviorResult<UpdateRoleCommandResponse>>
    {
        public Guid RoleId { get; set; }
        public string NewName { get; set; } = null!;
    }
}
