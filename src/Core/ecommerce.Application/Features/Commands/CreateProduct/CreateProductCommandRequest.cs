using ecommerce.Application.Models.ValueObjects;
using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<ValidationBehaviorResult<CreateProductCommandResponse>>
    {
        public string Name { get; set; } = null!;
        public List<MoneyModel> Prices { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
