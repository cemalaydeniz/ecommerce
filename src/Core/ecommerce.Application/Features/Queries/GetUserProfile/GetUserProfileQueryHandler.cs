using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.UserAggregate;
using MediatR;

namespace ecommerce.Application.Features.Queries.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQueryRequest, ValidationBehaviorResult<GetUserProfileQueryResponse>>
    {
        private readonly IUnitofWork _unitofWork;

        public GetUserProfileQueryHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ValidationBehaviorResult<GetUserProfileQueryResponse>> Handle(GetUserProfileQueryRequest request, CancellationToken cancellationToken)
        {
            User? user = await _unitofWork.UserRepository.GetByIdAsync(request.UserId, false, false, cancellationToken);
            if (user == null)
                return ValidationBehaviorResult<GetUserProfileQueryResponse>.Fail(ConstantsUtility.User.UserNotFound);

            return new GetUserProfileQueryResponse()
            {
                User = user
            };
        }
    }
}
