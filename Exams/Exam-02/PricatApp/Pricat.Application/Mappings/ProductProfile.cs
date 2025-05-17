using AutoMapper;
using Pricat.Application.DTOs.Product;
using Pricat.Domain.Entities;

namespace Pricat.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<CreateProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>();
        }
    }
}