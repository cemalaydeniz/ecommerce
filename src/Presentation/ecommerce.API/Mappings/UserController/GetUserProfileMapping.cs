using AutoMapper;
using ecommerce.API.Dtos.UserController;
using ecommerce.Application.Features.Queries.GetUserProfile;

namespace ecommerce.API.Mappings.UserController
{
    public class GetUserProfileMapping : Profile
    {
        public GetUserProfileMapping()
        {
            CreateMap<GetUserProfileQueryResponse, GetUserProfileDto>()
                .ForMember(d => d.Email, _ => _.MapFrom(s => s.User.Email))
                .ForMember(d => d.Name, _ => _.MapFrom(s => s.User.Name))
                .ForMember(d => d.PhoneNumber, _ => _.MapFrom(s => s.User.PhoneNumber))
                .ForMember(d => d.Addresses, _ => _.MapFrom(s => s.User.Addresses));
        }
    }
}
