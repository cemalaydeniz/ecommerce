using AutoMapper;
using ecommerce.API.Utilities.Json;
using ecommerce.API.Utilities;
using ecommerce.API.Utilities.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ecommerce.Application.Features.Commands.RefreshToken;
using ecommerce.API.Dtos.TokenController;

namespace ecommerce.API.Controller
{
    [ApiController]
    [Route("token")]
    [Authorize]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TokenController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPut]
        public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies[ConstantsUtility.Cookies.RefreshTokenKey];
            if (Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId) &&
                refreshToken != null)
            {
                var result = await _mediator.Send(new RefreshTokenCommandRequest()
                {
                    UserId = userId,
                    RefreshToken = refreshToken
                }, cancellationToken);

                if (result.IsSuccess)
                {
                    CookieUtility.AddToCookie(Response,
                        ConstantsUtility.Cookies.RefreshTokenKey,
                        result.Response!.Token.RefreshToken,
                        result.Response.Token.RefreshTokenExpirationDate,
                        true);

                    var response = _mapper.Map<RefreshTokenDto>(result.Response);
                    return Ok(JsonUtility.Payload(response, null, StatusCodes.Status200OK));
                }

                return BadRequest(JsonUtility.Fail(result.Errors, StatusCodes.Status400BadRequest));
            }

            return Unauthorized(JsonUtility.Fail(ConstantsUtility.TokenController.NotSignedIn, StatusCodes.Status401Unauthorized));
        }
    }
}
