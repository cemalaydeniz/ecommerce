using AutoMapper;
using ecommerce.API.Dtos.OrderController;
using ecommerce.Application.Features.Queries.GetOrders;
using ecommerce.Domain.Aggregates.OrderRepository;
using ecommerce.Domain.Aggregates.OrderRepository.Entities;

namespace ecommerce.API.Mappings.OrderController
{
    public class GetOrdersMapping : Profile
    {
        public GetOrdersMapping()
        {
            CreateMap<OrderItem, GetOrdersDto.OrderInfo.ItemInfo>();
            CreateMap<Order, GetOrdersDto.OrderInfo>()
                .ForMember(d => d.Items, _ => _.MapFrom(s => s.OrderItems));
            CreateMap<GetOrdersQueryResponse, GetOrdersDto>();
        }
    }
}
