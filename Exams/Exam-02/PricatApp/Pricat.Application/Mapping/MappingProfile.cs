using AutoMapper;
using Pricat.Application.Dtos;
using Pricat.Domain.Models;

namespace Pricat.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CategoryDto, Category>();
        CreateMap<Category, CategoryDto>();
        CreateMap<ProductDto, Product>();
        CreateMap<Product, ProductDto>();
    }
}