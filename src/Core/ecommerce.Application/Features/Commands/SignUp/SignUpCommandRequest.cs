using ecommerce.Application.Models.ValueObjects;
using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.SignUp
{
    public class SignUpCommandRequest : IRequest<ValidationBehaviorResult<SignUpCommandResponse>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public AddressModel? Address { get; set; }
    }
}
