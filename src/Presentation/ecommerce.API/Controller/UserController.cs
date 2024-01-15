using AutoMapper;
using ecommerce.API.Models.UserController;
using ecommerce.API.Utilities.Constants;
using ecommerce.API.Utilities.Json;
using ecommerce.Application.Features.Commands.SignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
