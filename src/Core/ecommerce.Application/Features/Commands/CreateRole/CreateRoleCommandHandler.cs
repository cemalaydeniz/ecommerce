using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.RoleAggregate;
using MediatR;

namespace ecommerce.Application.Features.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, ValidationBehaviorResult<CreateRoleCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;

        public CreateRoleCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ValidationBehaviorResult<CreateRoleCommandResponse>> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            Role? role = await _unitofWork.RoleRepository.GetByNameAsync(request.Name, false, cancellationToken);
            if (role != null)
                return ValidationBehaviorResult<CreateRoleCommandResponse>.Fail(ConstantsUtility.Role.RoleExists);

            Role newRole = new Role(request.Name);

            await _unitofWork.RoleRepository.AddAsync(newRole, cancellationToken);
            await _unitofWork.SaveChangesAsync(cancellationToken);

            return new CreateRoleCommandResponse()
            {
                RoleId = newRole.Id
            };
        }
    }
}
