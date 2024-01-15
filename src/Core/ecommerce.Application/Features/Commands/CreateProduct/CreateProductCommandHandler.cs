using AutoMapper;
using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Common.ValueObjects;
using MediatR;

namespace ecommerce.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, ValidationBehaviorResult<CreateProductCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public async Task<ValidationBehaviorResult<CreateProductCommandResponse>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product newProduct = new Product(request.Name,
                _mapper.Map<List<Money>>(request.Prices),
                request.Description);

            await _unitofWork.ProductRepository.AddAsync(newProduct, cancellationToken);
            await _unitofWork.SaveChangesAsync(cancellationToken);

            return new CreateProductCommandResponse()
            {
                ProductId = newProduct.Id
            };
        }
    }
}
