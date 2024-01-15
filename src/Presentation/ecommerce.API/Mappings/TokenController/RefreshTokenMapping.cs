using AutoMapper;
using ecommerce.API.Dtos.TokenController;
using ecommerce.Application.Features.Commands.RefreshToken;

namespace ecommerce.API.Mappings.TokenController
{
    public class RefreshTokenMapping : Profile
    {
        public RefreshTokenMapping()
        {
            CreateMap<RefreshTokenCommandResponse, RefreshTokenDto>()
                .ForMember(d => d.AccessToken, _ => _.MapFrom(s => s.Token.AccessToken));
        }
    }
}
