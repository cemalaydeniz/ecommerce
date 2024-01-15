using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.SoftDeleteProduct
{
    public class SoftDeleteProductCommandRequest : IRequest<ValidationBehaviorResult<SoftDeleteProductCommandResponse>>
    {
        public Guid ProductId { get; set; }
    }
}
