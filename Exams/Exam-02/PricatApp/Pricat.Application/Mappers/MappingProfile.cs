// En Pricat.Application.Mappings/MappingProfile.cs
using AutoMapper;
using Pricat.Application.DTOs;
using Pricat.Domain.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pricat.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();

            // Mapeo de Product
            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateDto, Product>();
            
        }
    }
}
