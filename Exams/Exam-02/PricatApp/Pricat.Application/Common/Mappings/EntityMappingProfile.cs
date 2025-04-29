using AutoMapper;
using Pricat.Application.DTOs;
using Pricat.Domain.Entities;

namespace Pricat.Application.Common.Mappings
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            CreateMap<CategoryDto, Category>();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.EanCode, opt => opt.MapFrom(src => src.EanCode))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit));
            CreateMap<ProductDto, Product>();
        }
    }
}