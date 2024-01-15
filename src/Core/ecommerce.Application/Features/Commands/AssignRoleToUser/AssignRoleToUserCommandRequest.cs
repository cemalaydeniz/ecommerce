using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.AssignRoleToUser
{
    public class AssignRoleToUserCommandRequest : IRequest<ValidationBehaviorResult<AssignRoleToUserCommandResponse>>
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
