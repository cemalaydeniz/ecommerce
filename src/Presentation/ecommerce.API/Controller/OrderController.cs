using AutoMapper;
using ecommerce.API.Dtos.OrderController;
using ecommerce.API.Utilities.Json;
using ecommerce.Application.Features.Queries.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.API.Controller
{
    [ApiController]
    [Route("order")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize(Roles = Application.Utilities.Constants.ConstantsUtility.Role.Admin)]
        [HttpGet("get")]
        public async Task<IActionResult> GetOrders([FromQuery]int page, [FromQuery]int pageSize, CancellationToken cancellationToken)
        {
            var request = new GetOrdersQueryRequest() { Page = page, PageSize = pageSize };
            var result = await _mediator.Send(request, cancellationToken);
            var dto = _mapper.Map<GetOrdersDto>(result);
            return Ok(JsonUtility.Payload(dto, null,
                dto.Orders.Count == 0 ? StatusCodes.Status204NoContent : StatusCodes.Status200OK));
        }
    }
}
