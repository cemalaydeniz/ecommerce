using AutoMapper;
using ecommerce.API.Models.ProductController;
using ecommerce.Application.Features.Commands.UpdateProduct;

namespace ecommerce.API.Mappings.ProductController
{
    public class UpdateProductMapping : Profile
    {
        public UpdateProductMapping()
        {
            CreateMap<UpdateProductModel, UpdateProductCommandRequest>();
        }
    }
}
