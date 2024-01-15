using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.UserAggregate;
using MediatR;

namespace ecommerce.Application.Features.Commands.UpdateCredentials
{
    public class UpdateCredentialsCommandHandler : IRequestHandler<UpdateCredentialsCommandRequest, ValidationBehaviorResult<UpdateCredentialsCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;

        public UpdateCredentialsCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ValidationBehaviorResult<UpdateCredentialsCommandResponse>> Handle(UpdateCredentialsCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await _unitofWork.UserRepository.GetByIdAsync(request.UserId, false, false, cancellationToken);
            if (user == null)
                return ValidationBehaviorResult<UpdateCredentialsCommandResponse>.Fail(ConstantsUtility.User.UserNotFound);

            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHashed))
                return ValidationBehaviorResult<UpdateCredentialsCommandResponse>.Fail(ConstantsUtility.Password.WrongPassword);

            bool isUpdated = false;
            if (!string.IsNullOrWhiteSpace(request.NewEmail))
            {
                User? conflictUser = await _unitofWork.UserRepository.GetByEmailAsync(request.NewEmail, false, false, cancellationToken);
                if (conflictUser != null)
                    return ValidationBehaviorResult<UpdateCredentialsCommandResponse>.Fail(ConstantsUtility.User.EmailInUse);

                if (user.UpdateEmail(request.NewEmail))
                {
                    isUpdated = true;
                }
            }
            if (!string.IsNullOrWhiteSpace(request.NewPassword))
            {
                string newPasswordHashed = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                if (user.UpdatePasswordHashed(newPasswordHashed))
                {
                    isUpdated = true;
                }
            }

            if (isUpdated)
            {
                user.ClearRefreshToken();

                _unitofWork.UserRepository.Update(user);
                await _unitofWork.SaveChangesAsync(cancellationToken);
            }

            return new UpdateCredentialsCommandResponse();
        }
    }
}
