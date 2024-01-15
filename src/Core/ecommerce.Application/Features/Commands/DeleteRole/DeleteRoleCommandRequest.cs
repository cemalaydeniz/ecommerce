using MediatR;

namespace ecommerce.Application.Features.Commands.DeleteRole
{
    public class DeleteRoleCommandRequest : IRequest<DeleteRoleCommandResponse>
    {
        public Guid RoleId { get; set; }
    }
}
