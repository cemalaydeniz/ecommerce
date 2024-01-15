using ecommerce.Application.Exceptions;
using ecommerce.Application.Services;
using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ecommerce.Application.Features.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, ValidationBehaviorResult<RefreshTokenCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<RefreshTokenCommandHandler> _logger;

        public RefreshTokenCommandHandler(IUnitofWork unitofWork,
            IJwtTokenService jwtTokenService,
            ILogger<RefreshTokenCommandHandler> logger)
        {
            _unitofWork = unitofWork;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        public async Task<ValidationBehaviorResult<RefreshTokenCommandResponse>> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await _unitofWork.UserRepository.GetByIdAsync(request.UserId, true, false, cancellationToken);
            if (user == null)
                return ValidationBehaviorResult<RefreshTokenCommandResponse>.Fail(ConstantsUtility.User.UserNotFound);

            if (user.RefreshToken == null || user.RefreshToken.Value != request.RefreshToken)
                return ValidationBehaviorResult<RefreshTokenCommandResponse>.Fail(ConstantsUtility.Authentication.NotAuthorized);

            var token = _jwtTokenService.GenerateToken(user, true);
            if (token == null)
            {
                _logger.LogCritical($"Could not create a token for the user whose ID is {user.Id}");
                throw new TokenCouldNotCreatedException();
            }

            user.UpdateRefreshToken(token.RefreshToken, token.RefreshTokenExpirationDate);

            _unitofWork.UserRepository.Update(user);
            await _unitofWork.SaveChangesAsync(cancellationToken);

            return new RefreshTokenCommandResponse()
            {
                Token = token
            };
        }
    }
}
