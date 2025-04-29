using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Application.DTOs;
using Pricat.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
