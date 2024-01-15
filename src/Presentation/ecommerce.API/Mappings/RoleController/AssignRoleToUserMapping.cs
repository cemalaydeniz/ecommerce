using AutoMapper;
using ecommerce.API.Models.RoleController;
using ecommerce.Application.Features.Commands.AssignRoleToUser;

namespace ecommerce.API.Mappings.RoleController
{
    public class AssignRoleToUserMapping : Profile
    {
        public AssignRoleToUserMapping()
        {
            CreateMap<AssignRoleToUserModel, AssignRoleToUserCommandRequest>();
        }
    }
}
