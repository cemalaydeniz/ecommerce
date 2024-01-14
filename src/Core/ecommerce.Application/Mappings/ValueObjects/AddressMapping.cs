using AutoMapper;
using ecommerce.Application.Models.ValueObjects;
using ecommerce.Domain.Common.ValueObjects;

namespace ecommerce.Application.Mappings.ValueObjects
{
    public class AddressMapping : Profile
    {
        public AddressMapping()
        {
            CreateMap<AddressModel, Address>()
                .ConstructUsing(s => new Address(s.Street, s.ZipCode, s.City, s.Country))
                .ReverseMap();
        }
    }
}
