using AutoMapper;
using ecommerce.API.Models.RoleController;
using ecommerce.Application.Features.Commands.CreateRole;

namespace ecommerce.API.Mappings.RoleController
{
    public class CreateRoleMapping : Profile
    {
        public CreateRoleMapping()
        {
            CreateMap<CreateRoleModel, CreateRoleCommandRequest>();
        }
    }
}
