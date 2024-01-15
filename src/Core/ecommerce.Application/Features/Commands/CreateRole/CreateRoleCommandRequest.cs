using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.CreateRole
{
    public class CreateRoleCommandRequest : IRequest<ValidationBehaviorResult<CreateRoleCommandResponse>>
    {
        public string Name { get; set; } = null!;
    }
}
