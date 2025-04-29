using AutoMapper;
using Pricat.Application.Dtos;
using Pricat.Domain.Models;

namespace Pricat.Application.Mapping
{
    // Clase que define el perfil de mapeo de AutoMapper
    public class MappingProfile : Profile
    {
        // Constructor donde se configuran los mapeos
        public MappingProfile()
        {
            // Mapeo bidireccional entre Category y CategoryDto
            CreateMap<Category, CategoryDto>().ReverseMap();

            // Mapeo bidireccional entre Product y ProductDto
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
