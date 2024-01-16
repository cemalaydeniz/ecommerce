using AutoMapper;
using ecommerce.API.Models.OrderController;
using ecommerce.Application.Features.Commands.AddTicketMessage;

namespace ecommerce.API.Mappings.OrderController
{
    public class AddTicketMessageMapping : Profile
    {
        public AddTicketMessageMapping()
        {
            CreateMap<AddTicketMessageModel, AddTicketMessageCommandRequest>();
        }
    }
}
