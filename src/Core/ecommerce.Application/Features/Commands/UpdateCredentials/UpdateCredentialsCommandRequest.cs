using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.UpdateCredentials
{
    public class UpdateCredentialsCommandRequest : IRequest<ValidationBehaviorResult<UpdateCredentialsCommandResponse>>
    {
        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; } = null!;
        public string? NewEmail { get; set; }
        public string? NewPassword { get; set; }
    }
}
