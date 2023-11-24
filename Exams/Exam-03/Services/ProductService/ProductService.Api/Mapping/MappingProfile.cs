using AutoMapper;
using ProductService.Api.Dtos;
using ProductService.Domain.Entities;

namespace ProductService.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductDto, Product>();
        CreateMap<Product, ProductDto>();
    }
}