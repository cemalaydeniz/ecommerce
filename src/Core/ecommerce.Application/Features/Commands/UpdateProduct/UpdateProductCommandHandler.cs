using AutoMapper;
using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Common.ValueObjects;
using MediatR;

namespace ecommerce.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, ValidationBehaviorResult<UpdateProductCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public async Task<ValidationBehaviorResult<UpdateProductCommandResponse>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _unitofWork.ProductRepository.GetByIdAsync(request.ProductId, false, cancellationToken);
            if (product == null)
                return ValidationBehaviorResult<UpdateProductCommandResponse>.Fail(ConstantsUtility.Product.ProductNotFound);

            bool isUpdated = false;
            if (!string.IsNullOrWhiteSpace(request.NewName) && product.UpdateName(request.NewName))
            {
                isUpdated = true;
            }
            if (!string.IsNullOrWhiteSpace(request.NewDescription) && product.UpdateDescription(request.NewDescription))
            {
                isUpdated = true;
            }

            bool isFree = product.Prices.Any(m => m.Amount == 0);
            if (request.CurrencyCodesToRemove != null && request.CurrencyCodesToRemove.Count != 0)
            {
                if (isFree)
                    return ValidationBehaviorResult<UpdateProductCommandResponse>.Fail(ConstantsUtility.Product.ProductIsFree);
                if (product.Prices.Count - request.CurrencyCodesToRemove.Count < 1)
                    return ValidationBehaviorResult<UpdateProductCommandResponse>.Fail(ConstantsUtility.Product.ProductMustHavePrice);

                foreach (var currencyCode in request.CurrencyCodesToRemove)
                {
                    if (product.RemovePrice(currencyCode))
                    {
                        isUpdated = true;
                    }
                }
            }
            if (request.PricesToUpdateOrAdd != null && request.PricesToUpdateOrAdd.Count != 0)
            {
                if (isFree)
                    return ValidationBehaviorResult<UpdateProductCommandResponse>.Fail(ConstantsUtility.Product.ProductIsFree);

                foreach (var price in request.PricesToUpdateOrAdd)
                {
                    var priceToUpdateOrAdd = _mapper.Map<Money>(price);
                    if (priceToUpdateOrAdd.Amount == 0)
                        return ValidationBehaviorResult<UpdateProductCommandResponse>.Fail(ConstantsUtility.Product.ProductIsPaid);

                    if (product.Prices.Any(m => m.CurrencyCode == price.CurrencyCode))
                    {
                        if (product.UpdatePrice(priceToUpdateOrAdd))
                        {
                            isUpdated = true;
                        }
                    }
                    else
                    {
                        if (product.AddPrice(priceToUpdateOrAdd))
                        {
                            isUpdated = true;
                        }
                    }
                }
            }

            if (isUpdated)
            {
                _unitofWork.ProductRepository.Update(product);
                await _unitofWork.SaveChangesAsync(cancellationToken);
            }

            return new UpdateProductCommandResponse();
        }
    }
}
