using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.RoleAggregate;
using MediatR;

namespace ecommerce.Application.Features.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, ValidationBehaviorResult<UpdateRoleCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;

        public UpdateRoleCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ValidationBehaviorResult<UpdateRoleCommandResponse>> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            Role? role = await _unitofWork.RoleRepository.GetByIdAsync(request.RoleId, false, cancellationToken);
            if (role == null)
                return ValidationBehaviorResult<UpdateRoleCommandResponse>.Fail(ConstantsUtility.Role.RoleNotFound);

            Role? conflictRole = await _unitofWork.RoleRepository.GetByNameAsync(request.NewName, false, cancellationToken);
            if (conflictRole != null)
                return ValidationBehaviorResult<UpdateRoleCommandResponse>.Fail(ConstantsUtility.Role.RoleExists);

            if (role.UpdateName(request.NewName))
            {
                _unitofWork.RoleRepository.Update(role);
                await _unitofWork.SaveChangesAsync(cancellationToken);
            }

            return new UpdateRoleCommandResponse();
        }
    }
}
