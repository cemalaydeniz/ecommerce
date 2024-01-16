using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.OrderAggregate;
using ecommerce.Domain.Aggregates.OrderAggregate.Entities;
using ecommerce.Domain.Aggregates.UserAggregate;
using MediatR;

namespace ecommerce.Application.Features.Commands.AddTicketMessage
{
    public class AddTicketMessageCommandHandler : IRequestHandler<AddTicketMessageCommandRequest, ValidationBehaviorResult<AddTicketMessageCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;

        public AddTicketMessageCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ValidationBehaviorResult<AddTicketMessageCommandResponse>> Handle(AddTicketMessageCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await _unitofWork.UserRepository.GetByIdAsync(request.UserId, true, false, cancellationToken);
            if (user == null)
                return ValidationBehaviorResult<AddTicketMessageCommandResponse>.Fail(ConstantsUtility.User.UserNotFound);

            Order? order = await _unitofWork.OrderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                return ValidationBehaviorResult<AddTicketMessageCommandResponse>.Fail(ConstantsUtility.Order.OrderNotFound);

            if (!order.UserId.Equals(user.Id) && !user.Roles.Any(r => r.Name == ConstantsUtility.Role.Admin))
                return ValidationBehaviorResult<AddTicketMessageCommandResponse>.Fail(ConstantsUtility.Authentication.NotAuthorized);

            TicketMessage newTicketMessage = new TicketMessage(user.Id, request.Content);
            if (order.AddMessageToTicket(newTicketMessage))
            {
                await _unitofWork.OrderRepository.AddTicketMessageAsync(order, newTicketMessage);
                await _unitofWork.SaveChangesAsync(cancellationToken);
            }
            else return ValidationBehaviorResult<AddTicketMessageCommandResponse>.Fail(ConstantsUtility.Order.TicketIsAlreadyClosed);

            return new AddTicketMessageCommandResponse();
        }
    }
}
