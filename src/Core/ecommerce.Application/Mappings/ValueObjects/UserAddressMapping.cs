using AutoMapper;
using ecommerce.Application.Models.ValueObjects;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Common.ValueObjects;

namespace ecommerce.Application.Mappings.ValueObjects
{
    public class UserAddressMapping : Profile
    {
        public UserAddressMapping()
        {
            CreateMap<UserAddressModel, UserAddress>()
                .ConstructUsing(s => new UserAddress(s.Title,
                    new Address(s.Address.Street, s.Address.ZipCode, s.Address.City, s.Address.Country)))
                .ReverseMap();
        }
    }
}
