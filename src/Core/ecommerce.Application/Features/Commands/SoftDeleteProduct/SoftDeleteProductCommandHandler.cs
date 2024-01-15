using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.ProductAggregate;
using MediatR;

namespace ecommerce.Application.Features.Commands.SoftDeleteProduct
{
    public class SoftDeleteProductCommandHandler : IRequestHandler<SoftDeleteProductCommandRequest, ValidationBehaviorResult<SoftDeleteProductCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;

        public SoftDeleteProductCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ValidationBehaviorResult<SoftDeleteProductCommandResponse>> Handle(SoftDeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _unitofWork.ProductRepository.GetByIdAsync(request.ProductId, false, cancellationToken);
            if (product == null)
                return ValidationBehaviorResult<SoftDeleteProductCommandResponse>.Fail(ConstantsUtility.Product.ProductNotFound);

            if (product.Delete())
            {
                _unitofWork.ProductRepository.Update(product);
                await _unitofWork.SaveChangesAsync();
            }

            return new SoftDeleteProductCommandResponse();
        }
    }
}
