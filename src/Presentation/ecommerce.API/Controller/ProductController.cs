using AutoMapper;
using ecommerce.API.Models.ProductController;
using ecommerce.API.Utilities.Constants;
using ecommerce.API.Utilities.Json;
using ecommerce.Application.Features.Commands.CreateProduct;
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
    }
}
