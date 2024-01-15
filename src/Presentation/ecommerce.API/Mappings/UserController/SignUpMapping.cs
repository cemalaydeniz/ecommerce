using AutoMapper;
using ecommerce.API.Models.UserController;
using ecommerce.Application.Features.Commands.SignUp;

namespace ecommerce.API.Mappings.UserController
{
    public class SignUpMapping : Profile
    {
        public SignUpMapping()
        {
            CreateMap<SignUpModel, SignUpCommandRequest>();
        }
    }
}
