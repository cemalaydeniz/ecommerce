using AutoMapper;
using ecommerce.API.Models.UserController;
using ecommerce.Application.Features.Commands.UpdateCredentials;

namespace ecommerce.API.Mappings.UserController
{
    public class UpdateCredentialsMapping : Profile
    {
        public UpdateCredentialsMapping()
        {
            CreateMap<UpdateCredentialsModel, UpdateCredentialsCommandRequest>();
        }
    }
}
