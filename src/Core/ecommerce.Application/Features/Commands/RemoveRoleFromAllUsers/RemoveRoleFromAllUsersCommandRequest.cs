using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.RemoveRoleFromAllUsers
{
    public class RemoveRoleFromAllUsersCommandRequest : IRequest<ValidationBehaviorResult<RemoveRoleFromAllUsersCommandResponse>>
    {
        public Guid RoleId { get; set; }
    }
}
