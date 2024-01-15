using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using MediatR;

namespace ecommerce.Application.Features.Commands.SoftDeleteUser
{
    public class SoftDeleteUserCommandHandler : IRequestHandler<SoftDeleteUserCommandRequest, ValidationBehaviorResult<SoftDeleteUserCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;

        public SoftDeleteUserCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ValidationBehaviorResult<SoftDeleteUserCommandResponse>> Handle(SoftDeleteUserCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await _unitofWork.UserRepository.GetByIdAsync(request.UserId, false, true, cancellationToken);
            if (user == null)
                return ValidationBehaviorResult<SoftDeleteUserCommandResponse>.Fail(ConstantsUtility.User.UserNotFound);

            if (user.Delete())
            {
                List<Role> rolesofUser = await _unitofWork.RoleRepository.GetRolesofUser(user.Id, cancellationToken);
                rolesofUser.ForEach(r => r.RemoveFromUser(user));

                _unitofWork.UserRepository.Update(user);
                await _unitofWork.SaveChangesAsync(cancellationToken);
            }

            return new SoftDeleteUserCommandResponse();
        }
    }
}
