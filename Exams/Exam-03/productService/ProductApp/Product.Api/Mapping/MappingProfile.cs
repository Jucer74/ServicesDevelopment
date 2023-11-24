using AutoMapper;
using Product.Api.Dtos;
using Product.Domain.Entities;

namespace Product.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EProduct, ProductDto>();
        CreateMap<ProductDto, EProduct>();
    }
}
