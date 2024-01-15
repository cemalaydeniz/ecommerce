using AutoMapper;
using ecommerce.API.Utilities.Json;
using ecommerce.API.Utilities.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ecommerce.API.Models.RoleController;
using ecommerce.Application.Features.Commands.CreateRole;

namespace ecommerce.API.Controller
{
    [ApiController]
    [Route("admin/roles")]
    [Authorize(Roles = Application.Utilities.Constants.ConstantsUtility.Role.Admin)]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RoleController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole([FromBody]CreateRoleModel model)
        {
            var request = _mapper.Map<CreateRoleCommandRequest>(model);
            var result = await _mediator.Send(request);
            return result.IsSuccess ?
                Ok(JsonUtility.Success(ConstantsUtility.RoleController.RoleCreated, StatusCodes.Status201Created)) :
                BadRequest(JsonUtility.Fail(result.Errors, StatusCodes.Status400BadRequest));
        }
    }
}
