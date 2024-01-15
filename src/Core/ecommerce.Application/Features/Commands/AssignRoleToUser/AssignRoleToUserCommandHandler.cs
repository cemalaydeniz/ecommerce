using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using MediatR;

namespace ecommerce.Application.Features.Commands.AssignRoleToUser
{
    public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommandRequest, ValidationBehaviorResult<AssignRoleToUserCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;

        public AssignRoleToUserCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ValidationBehaviorResult<AssignRoleToUserCommandResponse>> Handle(AssignRoleToUserCommandRequest request, CancellationToken cancellationToken)
        {
            Role? role = await _unitofWork.RoleRepository.GetByIdAsync(request.RoleId, true, cancellationToken);
            if (role == null)
                return ValidationBehaviorResult<AssignRoleToUserCommandResponse>.Fail(ConstantsUtility.Role.RoleNotFound);

            User? user = await _unitofWork.UserRepository.GetByIdAsync(request.UserId, false, true, cancellationToken);
            if (user == null)
                return ValidationBehaviorResult<AssignRoleToUserCommandResponse>.Fail(ConstantsUtility.User.UserNotFound);
            if (user.IsDeleted)
                return ValidationBehaviorResult<AssignRoleToUserCommandResponse>.Fail(ConstantsUtility.User.UserSoftDeleted);

            if (role.AssignToUser(user))
            {
                await _unitofWork.SaveChangesAsync(cancellationToken);
            }

            return new AssignRoleToUserCommandResponse();
        }
    }
}
