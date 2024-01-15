using AutoMapper;
using ecommerce.API.Dtos.PaymentController;
using ecommerce.API.Models.PaymentController;
using ecommerce.Application.Features.Queries.InitiatePayment;

namespace ecommerce.API.Mappings.PaymentController
{
    public class InitiatePaymentMapping : Profile
    {
        public InitiatePaymentMapping()
        {
            CreateMap<InitiatePaymentModel.Item, InitiatePaymentQueryRequest.Item>();
            CreateMap<InitiatePaymentModel, InitiatePaymentQueryRequest>();
            CreateMap<InitiatePaymentQueryResponse, InitiatePaymentDto>()
                .ForMember(d => d.ClientSecret, _ => _.MapFrom(s => s.PaymentIntent.ClientSecret));
        }
    }
}
