using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.OrderAggregate;
using MediatR;

namespace ecommerce.Application.Features.Commands.CloseTicket
{
    public class CloseTicketCommandHandler : IRequestHandler<CloseTicketCommandRequest, ValidationBehaviorResult<CloseTicketCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;

        public CloseTicketCommandHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<ValidationBehaviorResult<CloseTicketCommandResponse>> Handle(CloseTicketCommandRequest request, CancellationToken cancellationToken)
        {
            Order? order = await _unitofWork.OrderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                return ValidationBehaviorResult<CloseTicketCommandResponse>.Fail(ConstantsUtility.Order.OrderNotFound);

            if (order.CloseTicket())
            {
                _unitofWork.OrderRepository.Update(order);
                await _unitofWork.SaveChangesAsync(cancellationToken);
            }

            return new CloseTicketCommandResponse();
        }
    }
}
