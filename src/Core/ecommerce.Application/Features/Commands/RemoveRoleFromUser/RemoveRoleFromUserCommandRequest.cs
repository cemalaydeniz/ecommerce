using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.RemoveRoleFromUser
{
    public class RemoveRoleFromUserCommandRequest : IRequest<ValidationBehaviorResult<RemoveRoleFromUserCommandResponse>>
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
