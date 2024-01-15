using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.ProductAggregate;
using MediatR;

namespace ecommerce.Application.Features.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, ValidationBehaviorResult<GetProductQueryResponse>>
    {
        private readonly IUnitofWork _unitofWork;

        public GetProductQueryHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ValidationBehaviorResult<GetProductQueryResponse>> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _unitofWork.ProductRepository.GetByIdAsync(request.ProductId, false, cancellationToken);
            if (product == null)
                return ValidationBehaviorResult<GetProductQueryResponse>.Fail(ConstantsUtility.Product.ProductNotFound);

            return new GetProductQueryResponse()
            {
                Product = product
            };
        }
    }
}
