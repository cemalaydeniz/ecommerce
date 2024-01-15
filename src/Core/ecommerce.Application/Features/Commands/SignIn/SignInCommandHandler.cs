using ecommerce.Application.Exceptions;
using ecommerce.Application.Services;
using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ecommerce.Application.Features.Commands.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommandRequest, ValidationBehaviorResult<SignInCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<SignInCommandHandler> _logger;

        public SignInCommandHandler(IUnitofWork unitofWork,
            IJwtTokenService jwtTokenService,
            ILogger<SignInCommandHandler> logger)
        {
            _unitofWork = unitofWork;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        public async Task<ValidationBehaviorResult<SignInCommandResponse>> Handle(SignInCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await _unitofWork.UserRepository.GetByEmailAsync(request.Email, true, false, cancellationToken);
            if (user == null)
                return ValidationBehaviorResult<SignInCommandResponse>.Fail(ConstantsUtility.User.UserNotFound);

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHashed))
                return ValidationBehaviorResult<SignInCommandResponse>.Fail(ConstantsUtility.Password.WrongPassword);

            var token = _jwtTokenService.GenerateToken(user, true);
            if (token == null)
            {
                _logger.LogCritical($"Could not create a token for the user whose ID is {user.Id}");
                throw new TokenCouldNotCreatedException();
            }

            user.UpdateRefreshToken(token.RefreshToken, token.RefreshTokenExpirationDate);

            _unitofWork.UserRepository.Update(user);
            await _unitofWork.SaveChangesAsync(cancellationToken);

            return new SignInCommandResponse()
            {
                Token = token
            };
        }
    }
}
