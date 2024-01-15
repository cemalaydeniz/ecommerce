using ecommerce.Application.Models.ValueObjects;
using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommandRequest : IRequest<ValidationBehaviorResult<UpdateProductCommandResponse>>
    {
        public Guid ProductId { get; set; }
        public string? NewName { get; set; }
        public string? NewDescription { get; set; }
        public List<string>? CurrencyCodesToRemove { get; set; }
        public List<MoneyModel>? PricesToUpdateOrAdd { get; set; }
    }
}
