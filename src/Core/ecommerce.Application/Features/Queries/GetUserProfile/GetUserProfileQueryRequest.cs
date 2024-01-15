using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Queries.GetUserProfile
{
    public class GetUserProfileQueryRequest : IRequest<ValidationBehaviorResult<GetUserProfileQueryResponse>>
    {
        public Guid UserId { get; set; }
    }
}
