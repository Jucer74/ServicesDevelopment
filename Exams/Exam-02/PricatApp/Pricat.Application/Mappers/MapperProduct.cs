using AutoMapper;
using Pricat.Application.DTOs;
using Pricat.Domain.Entities;

namespace Pricat.Application.Mappering
{
    public class MapperProduct : Profile
    {
        public MapperProduct()
        {
            CreateMap<ProductDto, Products>().ReverseMap();
            CreateMap<CrearProductDto, Products>();
        }
    }
}