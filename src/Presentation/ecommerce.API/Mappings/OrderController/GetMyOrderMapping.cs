using AutoMapper;
using ecommerce.API.Dtos.OrderController;
using ecommerce.Application.Features.Queries.GetMyOrders;
using ecommerce.Domain.Aggregates.OrderRepository;
using ecommerce.Domain.Aggregates.OrderRepository.Entities;

namespace ecommerce.API.Mappings.OrderController
{
    public class GetMyOrderMapping : Profile
    {
        public GetMyOrderMapping()
        {
            CreateMap<OrderItem, GetMyOrdersDto.OrderInfo.ItemInfo>();
            CreateMap<Order, GetMyOrdersDto.OrderInfo>()
                .ForMember(d => d.Items, _ => _.MapFrom(s => s.OrderItems));
            CreateMap<GetMyOrdersQueryResponse, GetMyOrdersDto>();
        }
    }
}
