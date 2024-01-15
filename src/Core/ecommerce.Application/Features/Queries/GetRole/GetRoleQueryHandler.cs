using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.RoleAggregate;
using MediatR;

namespace ecommerce.Application.Features.Queries.GetRole
{
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQueryRequest, ValidationBehaviorResult<GetRoleQueryResponse>>
    {
        private readonly IUnitofWork _unitofWork;

        public GetRoleQueryHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ValidationBehaviorResult<GetRoleQueryResponse>> Handle(GetRoleQueryRequest request, CancellationToken cancellationToken)
        {
            Role? role = await _unitofWork.RoleRepository.GetByIdAsync(request.RoleId, request.GetUsers, cancellationToken);
            if (role == null)
                return ValidationBehaviorResult<GetRoleQueryResponse>.Fail(ConstantsUtility.Role.RoleNotFound);

            return new GetRoleQueryResponse()
            {
                Role = role
            };
        }
    }
}
