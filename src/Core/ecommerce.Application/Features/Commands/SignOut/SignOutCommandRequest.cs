using MediatR;

namespace ecommerce.Application.Features.Commands.SignOut
{
    public class SignOutCommandRequest : IRequest<SignOutCommandResponse>
    {
        public Guid UserId { get; set; }
        public bool SignOutAllDevices { get; set; }
    }
}
