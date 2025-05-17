using AutoMapper;
using Pricat.Application.DTOs.Category;
using Pricat.Domain.Entities;

namespace Pricat.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();
        }
    }
}