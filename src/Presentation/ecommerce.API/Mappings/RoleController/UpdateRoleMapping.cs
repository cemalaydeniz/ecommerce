using AutoMapper;
using ecommerce.API.Models.RoleController;
using ecommerce.Application.Features.Commands.UpdateRole;

namespace ecommerce.API.Mappings.RoleController
{
    public class UpdateRoleMapping : Profile
    {
        public UpdateRoleMapping()
        {
            CreateMap<UpdateRoleModel, UpdateRoleCommandRequest>();
        }
    }
}
