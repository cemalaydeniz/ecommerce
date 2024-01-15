using AutoMapper;
using ecommerce.API.Models.UserController;
using ecommerce.Application.Features.Commands.UpdateProfile;

namespace ecommerce.API.Mappings.UserController
{
    public class UpdateProfileMapping : Profile
    {
        public UpdateProfileMapping()
        {
            CreateMap<UpdateProfileModel, UpdateProfileCommandRequest>();
        }
    }
}
