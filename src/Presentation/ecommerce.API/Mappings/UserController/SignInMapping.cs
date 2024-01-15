using AutoMapper;
using ecommerce.API.Dtos.UserController;
using ecommerce.API.Models.UserController;
using ecommerce.Application.Features.Commands.SignIn;

namespace ecommerce.API.Mappings.UserController
{
    public class SignInMapping : Profile
    {
        public SignInMapping()
        {
            CreateMap<SignInModel, SignInCommandRequest>();
            CreateMap<SignInCommandResponse, SignInDto>()
                .ForMember(d => d.AccessToken, _ => _.MapFrom(s => s.Token.AccessToken));
        }
    }
}
