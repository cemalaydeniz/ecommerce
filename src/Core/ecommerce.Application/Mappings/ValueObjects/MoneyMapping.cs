using AutoMapper;
using ecommerce.Application.Models.ValueObjects;
using ecommerce.Domain.Common.ValueObjects;

namespace ecommerce.Application.Mappings.ValueObjects
{
    public class MoneyMapping : Profile
    {
        public MoneyMapping()
        {
            CreateMap<MoneyModel, Money>()
                .ConstructUsing(s => new Money(s.CurrencyCode, s.Amount))
                .ReverseMap();
        }
    }
}
