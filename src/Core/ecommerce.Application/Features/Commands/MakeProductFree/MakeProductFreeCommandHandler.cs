using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.ProductAggregate;
using MediatR;

namespace ecommerce.Application.Features.Commands.MakeProductFree
{
    public class MakeProductFreeCommandHandler : IRequestHandler<MakeProductFreeCommandRequest, ValidationBehaviorResult<MakeProductFreeCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;

        public MakeProductFreeCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ValidationBehaviorResult<MakeProductFreeCommandResponse>> Handle(MakeProductFreeCommandRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _unitofWork.ProductRepository.GetByIdAsync(request.ProductId, false, cancellationToken);
            if (product == null)
                return ValidationBehaviorResult<MakeProductFreeCommandResponse>.Fail(ConstantsUtility.Product.ProductNotFound);

            if (product.MakeFree())
            {
                _unitofWork.ProductRepository.Update(product);
                await _unitofWork.SaveChangesAsync(cancellationToken);
            }

            return new MakeProductFreeCommandResponse();
        }
    }
}
