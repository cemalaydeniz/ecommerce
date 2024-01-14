using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ecommerce.Application.Events.UserEvents.UserCreatedEvent
{
    public class AddDefaultRoleHandler : INotificationHandler<UserCreated>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<AddDefaultRoleHandler> _logger;

        public AddDefaultRoleHandler(IUnitofWork unitofWork,
            ILogger<AddDefaultRoleHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
        {
            Role? role = await _unitofWork.RoleRepository.GetByNameAsync(ConstantsUtility.Role.DefaultRole, false, cancellationToken);
            if (role == null)
            {
                _logger.LogCritical($"The default role {ConstantsUtility.Role.DefaultRole} was not found or soft deleted. The user whose ID is {notification.CreatedUser.Id} has currently no roles");

                /**
                 * The users who do not have default roles can be handled here or can be saved somewhere else to handle them later
                 */
                return;
            }

            role.AssignToUser(notification.CreatedUser);

            // This domain event handler will not fire another domain event
            role.ClearDomainEvents();
        }
    }
}
