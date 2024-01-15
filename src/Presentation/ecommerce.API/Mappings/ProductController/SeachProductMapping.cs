using AutoMapper;
using ecommerce.API.Dtos.ProductController;
using ecommerce.Application.Features.Queries.SearchProducts;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Common.ValueObjects;

namespace ecommerce.API.Mappings.ProductController
{
    public class SeachProductMapping : Profile
    {
        public SeachProductMapping()
        {
            CreateMap<Product, SearchProductDto.ProductInfo>()
                .ForMember(d => d.Prices, _ => _.MapFrom(s => s.Prices.Select(m => new Money(m.CurrencyCode, m.Amount))));
            CreateMap<SearchProductsQueryResponse, SearchProductDto>()
                .ForMember(d => d.Result, _ => _.MapFrom(s => s.Products));
        }
    }
}
