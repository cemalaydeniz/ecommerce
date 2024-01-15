using AutoMapper;
using ecommerce.API.Dtos.PaymentController;
using ecommerce.API.Models.PaymentController;
using ecommerce.API.Utilities.Constants;
using ecommerce.API.Utilities.Json;
using ecommerce.Application.Features.Queries.InitiatePayment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ecommerce.API.Controller
{
    [ApiController]
    [Route("payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PaymentController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> InitiatePayment([FromBody]InitiatePaymentModel model, CancellationToken cancellationToken)
        {
            if (Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
            {
                var request = _mapper.Map<InitiatePaymentQueryRequest>(model);
                request.UserId = userId;
                var result = await _mediator.Send(request, cancellationToken);
                if (result.IsSuccess)
                {
                    var dto = _mapper.Map<InitiatePaymentDto>(result.Response);
                    return Ok(JsonUtility.Payload(dto, null, StatusCodes.Status200OK));
                }

                return BadRequest(JsonUtility.Fail(result.Errors, StatusCodes.Status400BadRequest));
            }

            return Unauthorized(JsonUtility.Fail(ConstantsUtility.PaymentController.NotSignedIn, StatusCodes.Status401Unauthorized));
        }
    }
}
