using ecommerce.Application.UnitofWorks;
using ecommerce.Domain.Aggregates.UserAggregate;
using MediatR;

namespace ecommerce.Application.Features.Commands.SignOut
{
    public class SignOutCommandHandler : IRequestHandler<SignOutCommandRequest, SignOutCommandResponse>
    {
        private readonly IUnitofWork _unitofWork;

        public SignOutCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<SignOutCommandResponse> Handle(SignOutCommandRequest request, CancellationToken cancellationToken)
        {
            /**
             * Depending on the business logic, the access token can be added to a blacklist. In this example, access
             * tokens will not be added to a blacklist. Only the sign out request will be checked if the refresh token
             * should be invalidated or not
             */

            if (request.SignOutAllDevices)
            {
                User? user = await _unitofWork.UserRepository.GetByIdAsync(request.UserId, false, false, cancellationToken);
                if (user != null)
                {
                    user.ClearRefreshToken();

                    _unitofWork.UserRepository.Update(user);
                    await _unitofWork.SaveChangesAsync(cancellationToken);
                }
            }

            return new SignOutCommandResponse();
        }
    }
}
