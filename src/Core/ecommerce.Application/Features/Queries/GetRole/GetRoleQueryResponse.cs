using ecommerce.Domain.Aggregates.RoleAggregate;

namespace ecommerce.Application.Features.Queries.GetRole
{
    public class GetRoleQueryResponse
    {
        public Role Role { get; set; } = null!;
    }
}
