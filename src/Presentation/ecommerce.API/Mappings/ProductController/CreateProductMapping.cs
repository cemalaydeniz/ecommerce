using AutoMapper;
using ecommerce.API.Models.ProductController;
using ecommerce.Application.Features.Commands.CreateProduct;

namespace ecommerce.API.Mappings.ProductController
{
    public class CreateProductMapping : Profile
    {
        public CreateProductMapping()
        {
            CreateMap<CreateProductModel, CreateProductCommandRequest>();
        }
    }
}
