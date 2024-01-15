using ecommerce.Application.UnitofWorks;
using MediatR;

namespace ecommerce.Application.Features.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
    {
        private readonly IUnitofWork _unitofWork;

        public DeleteRoleCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
        {
            await _unitofWork.RoleRepository.Delete(request.RoleId, cancellationToken);
            await _unitofWork.SaveChangesAsync(cancellationToken);

            return new DeleteRoleCommandResponse();
        }
    }
}
