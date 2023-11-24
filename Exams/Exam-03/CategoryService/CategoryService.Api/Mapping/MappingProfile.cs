using AutoMapper;
using CategoryService.Api.Dtos;
using CategoryService.Domain.Entities;

namespace CategoryService.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
