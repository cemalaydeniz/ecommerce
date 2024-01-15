using ecommerce.Application.Models.ValueObjects;
using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.UpdateProfile
{
    public class UpdateProfileCommandRequest : IRequest<ValidationBehaviorResult<UpdateProfileCommandResponse>>
    {
        public Guid UserId { get; set; }
        public string? NewName { get; set; }
        public string? NewPhoneNumber { get; set; }
        public string? TitleofAddressToUpdate { get; set; }
        public UserAddressModel? UserAddress { get; set; }
    }
}
