using ecommerce.Domain.Aggregates.UserAggregate;

namespace ecommerce.Application.Features.Queries.GetUserProfile
{
    public class GetUserProfileQueryResponse
    {
        public User User { get; set; } = null!;
    }
}
