using AutoMapper;
using ecommerce.API.Dtos.UserController;
using ecommerce.API.Models.UserController;
using ecommerce.API.Utilities;
using ecommerce.API.Utilities.Constants;
using ecommerce.API.Utilities.Json;
using ecommerce.Application.Features.Commands.SignIn;
using ecommerce.Application.Features.Commands.SignOut;
using ecommerce.Application.Features.Commands.SignUp;
using ecommerce.Application.Features.Commands.UpdateProfile;
using ecommerce.Application.Features.Queries.GetUserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ecommerce.API.Controller
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            var request = _mapper.Map<SignUpCommandRequest>(model);
            var result = await _mediator.Send(request);
            return result.IsSuccess ?
                Ok(JsonUtility.Success(ConstantsUtility.UserController.SignedUp, StatusCodes.Status201Created)) :
                BadRequest(JsonUtility.Fail(result.Errors, StatusCodes.Status400BadRequest));
        }

        [HttpPut("sign-in")]
        public async Task<IActionResult> SignIn([FromBody]SignInModel model, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<SignInCommandRequest>(model);
            var result = await _mediator.Send(request, cancellationToken);
            if (result.IsSuccess)
            {
                CookieUtility.AddToCookie(Response,
                        ConstantsUtility.Cookies.RefreshTokenKey,
                        result.Response!.Token.RefreshToken,
                        result.Response.Token.RefreshTokenExpirationDate,
                        true);

                var dto = _mapper.Map<SignInDto>(result.Response);
                return Ok(JsonUtility.Payload(dto, ConstantsUtility.UserController.SignedIn, StatusCodes.Status200OK));
            }

            return BadRequest(JsonUtility.Fail(result.Errors, StatusCodes.Status400BadRequest));
        }

        [HttpPut("sign-out")]
        public async Task<IActionResult> SignOut([FromQuery]bool signOutAllDevices)
        {
            if (Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
            {
                await _mediator.Send(new SignOutCommandRequest()
                {
                    UserId = userId,
                    SignOutAllDevices = signOutAllDevices
                });
            }

            return Ok(JsonUtility.Success(ConstantsUtility.UserController.SignedOut, StatusCodes.Status200OK));
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
        {
            if (Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
            {
                var request = new GetUserProfileQueryRequest() { UserId = userId };
                var result = await _mediator.Send(request, cancellationToken);
                if (result.IsSuccess)
                {
                    var dto = _mapper.Map<GetUserProfileDto>(result.Response);
                    return Ok(JsonUtility.Payload(dto, null, StatusCodes.Status200OK));
                }

                return BadRequest(JsonUtility.Fail(result.Errors, StatusCodes.Status400BadRequest));
            }

            return Unauthorized(JsonUtility.Fail(ConstantsUtility.UserController.NotSignedIn, StatusCodes.Status401Unauthorized));
        }

        [Authorize]
        [HttpPut("profile/update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody]UpdateProfileModel model)
        {
            if (Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
            {
                var request = _mapper.Map<UpdateProfileCommandRequest>(model);
                request.UserId = userId;
                var result = await _mediator.Send(request);
                return result.IsSuccess ?
                    Ok(JsonUtility.Success(ConstantsUtility.UserController.ProfileUpdated, StatusCodes.Status200OK)) :
                    BadRequest(JsonUtility.Fail(result.Errors, StatusCodes.Status400BadRequest));
            }

            return Unauthorized(JsonUtility.Fail(ConstantsUtility.UserController.NotSignedIn, StatusCodes.Status401Unauthorized));
        }
    }
}
