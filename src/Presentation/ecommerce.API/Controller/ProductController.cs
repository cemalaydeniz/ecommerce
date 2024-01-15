using AutoMapper;
using ecommerce.API.Dtos.ProductController;
using ecommerce.API.Models.ProductController;
using ecommerce.API.Utilities.Constants;
using ecommerce.API.Utilities.Json;
using ecommerce.Application.Features.Commands.CreateProduct;
using ecommerce.Application.Features.Queries.GetProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.API.Controller
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize(Roles = Application.Utilities.Constants.ConstantsUtility.Role.Admin)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody]CreateProductModel model)
        {
            var request = _mapper.Map<CreateProductCommandRequest>(model);
            var result = await _mediator.Send(request);
            return result.IsSuccess ?
                Ok(JsonUtility.Success(ConstantsUtility.ProductController.ProductCreated, StatusCodes.Status201Created)) :
                BadRequest(JsonUtility.Fail(result.Errors, StatusCodes.Status400BadRequest));
        }

        [HttpGet("get/{productId}")]
        public async Task<IActionResult> GetProduct(Guid productId, CancellationToken cancellationToken)
        {
            var request = new GetProductQueryRequest() { ProductId = productId };
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
            {
                var dto = _mapper.Map<GetProductDto>(result.Response);
                return Ok(JsonUtility.Payload(dto, null, StatusCodes.Status200OK));
            }

            return BadRequest(JsonUtility.Fail(result.Errors, StatusCodes.Status400BadRequest));
        }
    }
}
