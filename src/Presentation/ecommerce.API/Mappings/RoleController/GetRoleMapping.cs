using AutoMapper;
using ecommerce.API.Dtos.RoleController;
using ecommerce.Application.Features.Queries.GetRole;
using ecommerce.Domain.Aggregates.UserAggregate;

namespace ecommerce.API.Mappings.RoleController
{
    public class GetRoleMapping : Profile
    {
        public GetRoleMapping()
        {
            CreateMap<User, GetRoleDto.UserInfo>();
            CreateMap<GetRoleQueryResponse, GetRoleDto>()
                .ForMember(d => d.Name, _ => _.MapFrom(s => s.Role.Name))
                .ForMember(d => d.Users, _ => _.MapFrom(s => s.Role.Users));
        }
    }
}
