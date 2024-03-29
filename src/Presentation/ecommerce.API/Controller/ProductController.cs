﻿using AutoMapper;
using ecommerce.API.Dtos.ProductController;
using ecommerce.API.Models.ProductController;
using ecommerce.API.Utilities.Constants;
using ecommerce.API.Utilities.Json;
using ecommerce.Application.Features.Commands.CreateProduct;
using ecommerce.Application.Features.Commands.MakeProductFree;
using ecommerce.Application.Features.Commands.SoftDeleteProduct;
using ecommerce.Application.Features.Commands.UpdateProduct;
using ecommerce.Application.Features.Queries.GetProduct;
using ecommerce.Application.Features.Queries.SearchProducts;
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

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery]string name, [FromQuery]int page, CancellationToken cancellationToken)
        {
            var request = new SearchProductsQueryRequest() { Name = name, Page = page };
            var result = await _mediator.Send(request, cancellationToken);
            var dto = _mapper.Map<SearchProductDto>(result);
            return Ok(JsonUtility.Payload(dto, null,
                dto.Result.Count == 0 ? StatusCodes.Status204NoContent : StatusCodes.Status200OK));
        }

        [Authorize(Roles = Application.Utilities.Constants.ConstantsUtility.Role.Admin)]
        [HttpPut("update/{productId}")]
        public async Task<IActionResult> UpdateProduct([FromBody]UpdateProductModel model, Guid productId)
        {
            var request = _mapper.Map<UpdateProductCommandRequest>(model);
            request.ProductId = productId;
            var result = await _mediator.Send(request);
            return result.IsSuccess ?
                Ok(JsonUtility.Success(ConstantsUtility.ProductController.ProductUpdated, StatusCodes.Status201Created)) :
                BadRequest(JsonUtility.Fail(result.Errors, StatusCodes.Status400BadRequest));
        }

        [Authorize(Roles = Application.Utilities.Constants.ConstantsUtility.Role.Admin)]
        [HttpPut("update/make-free/{productId}")]
        public async Task<IActionResult> MakeFree(Guid productId)
        {
            var request = new MakeProductFreeCommandRequest() { ProductId = productId };
            var result = await _mediator.Send(request);
            return result.IsSuccess ?
                Ok(JsonUtility.Success(ConstantsUtility.ProductController.ProductMadeFree, StatusCodes.Status200OK)) :
                BadRequest(JsonUtility.Fail(result.Errors, StatusCodes.Status400BadRequest));
        }

        [Authorize(Roles = Application.Utilities.Constants.ConstantsUtility.Role.Admin)]
        [HttpPut("soft-delete/{productId}")]
        public async Task<IActionResult> SoftDeleteProduct(Guid productId)
        {
            var request = new SoftDeleteProductCommandRequest() { ProductId = productId };
            var result = await _mediator.Send(request);
            return result.IsSuccess ?
                Ok(JsonUtility.Success(ConstantsUtility.ProductController.ProductSoftDeleted, StatusCodes.Status200OK)) :
                BadRequest(JsonUtility.Fail(result.Errors, StatusCodes.Status400BadRequest));
        }
    }
}
