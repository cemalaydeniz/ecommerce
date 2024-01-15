using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.RoleAggregate;
using MediatR;

namespace ecommerce.Application.Features.Commands.RemoveRoleFromAllUsers
{
    public class RemoveRoleFromAllUsersCommandHandler : IRequestHandler<RemoveRoleFromAllUsersCommandRequest, ValidationBehaviorResult<RemoveRoleFromAllUsersCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;

        public RemoveRoleFromAllUsersCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ValidationBehaviorResult<RemoveRoleFromAllUsersCommandResponse>> Handle(RemoveRoleFromAllUsersCommandRequest request, CancellationToken cancellationToken)
        {
            Role? role = await _unitofWork.RoleRepository.GetByIdAsync(request.RoleId, true, cancellationToken);
            if (role == null)
                return ValidationBehaviorResult<RemoveRoleFromAllUsersCommandResponse>.Fail(ConstantsUtility.Role.RoleNotFound);

            if (role.RemoveFromAllUsers())
            {
                await _unitofWork.SaveChangesAsync(cancellationToken);
            }

            return new RemoveRoleFromAllUsersCommandResponse();
        }
    }
}
