using AutoMapper;
using Pricat.Application.DTOs;
using Pricat.Domain.Entities;

namespace Pricat.Application.Mappering
{
    public class MapperCategory : Profile
    {
        public MapperCategory()
        {
            CreateMap<Categories, CategoryDto>().ReverseMap();
            CreateMap<Categories, CrearCategoryDto>().ReverseMap();
        }
    }
}
