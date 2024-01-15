using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using MediatR;

namespace ecommerce.Application.Features.Commands.RemoveRoleFromUser
{
    public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommandRequest, ValidationBehaviorResult<RemoveRoleFromUserCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;

        public RemoveRoleFromUserCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ValidationBehaviorResult<RemoveRoleFromUserCommandResponse>> Handle(RemoveRoleFromUserCommandRequest request, CancellationToken cancellationToken)
        {
            Role? role = await _unitofWork.RoleRepository.GetByIdAsync(request.RoleId, true, cancellationToken);
            if (role == null)
                return ValidationBehaviorResult<RemoveRoleFromUserCommandResponse>.Fail(ConstantsUtility.Role.RoleNotFound);

            User? user = await _unitofWork.UserRepository.GetByIdAsync(request.UserId, false, true, cancellationToken);
            if (user == null)
                return ValidationBehaviorResult<RemoveRoleFromUserCommandResponse>.Fail(ConstantsUtility.User.UserNotFound);
            if (user.IsDeleted)
                return ValidationBehaviorResult<RemoveRoleFromUserCommandResponse>.Fail(ConstantsUtility.User.UserSoftDeleted);

            if (role.Users.Any(u => u == user) && role.RemoveFromUser(user))
            {
                await _unitofWork.SaveChangesAsync(cancellationToken);
            }

            return new RemoveRoleFromUserCommandResponse();
        }
    }
}
