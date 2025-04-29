using AutoMapper;
using Pricat.Application.DTO;
using Pricat.Domain.Entities;


namespace Pricat.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeo entre ProductCreateDto y Product
            CreateMap<ProductCreateDto, Product>().ReverseMap();

            // Mapeo entre ProductResultDto y Product
            CreateMap<Product, ProductResultDto>().ReverseMap();
            // Mapeo entre CategoryCreateDto y Category
            CreateMap<CategoryCreateDto, Category>().ReverseMap();

            // Mapeo entre CategoryResultDto y Category
            CreateMap<Category, CategoryResultDto>().ReverseMap();
        }
    }
}
