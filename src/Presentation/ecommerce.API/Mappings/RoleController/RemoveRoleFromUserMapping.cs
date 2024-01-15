using AutoMapper;
using ecommerce.API.Models.RoleController;
using ecommerce.Application.Features.Commands.RemoveRoleFromUser;

namespace ecommerce.API.Mappings.RoleController
{
    public class RemoveRoleFromUserMapping : Profile
    {
        public RemoveRoleFromUserMapping()
        {
            CreateMap<RemoveRoleFromUserModel, RemoveRoleFromUserCommandRequest>();
        }
    }
}
