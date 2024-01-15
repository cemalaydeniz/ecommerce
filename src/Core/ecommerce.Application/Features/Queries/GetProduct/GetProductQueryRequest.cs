using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Queries.GetProduct
{
    public class GetProductQueryRequest : IRequest<ValidationBehaviorResult<GetProductQueryResponse>>
    {
        public Guid ProductId { get; set; }
    }
}
