using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Queries.GetRole
{
    public class GetRoleQueryRequest : IRequest<ValidationBehaviorResult<GetRoleQueryResponse>>
    {
        public Guid RoleId { get; set; }
        public bool GetUsers { get; set; }
    }
}
