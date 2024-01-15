using AutoMapper;
using ecommerce.API.Dtos.ProductController;
using ecommerce.Application.Features.Queries.GetProduct;
using ecommerce.Domain.Common.ValueObjects;

namespace ecommerce.API.Mappings.ProductController
{
    public class GetProductMapping : Profile
    {
        public GetProductMapping()
        {
            CreateMap<GetProductQueryResponse, GetProductDto>()
                .ForMember(d => d.Name, _ => _.MapFrom(s => s.Product.Name))
                .ForMember(d => d.Description, _ => _.MapFrom(s => s.Product.Description))
                .ForMember(d => d.Prices, _ => _.MapFrom(s => s.Product.Prices.Select(m => new Money(m.CurrencyCode, m.Amount))));
        }
    }
}
