using AutoMapper;
using CategoryService.Api.Dtos;
using CategoryService.Api.Mapping;
using CategoryService.Api.Validators;
using CategoryService.Application.Interfaces;
using CategoryService.Application.Services;
using CategoryService.Domain.Interfaces.Repositories;
using CategoryService.Infrastructure.Repositories;
using FluentValidation;

namespace CategoryService.Api.Extensions
{
    public static class ModuleCollectionExtension
    {
        public static IServiceCollection AddCoreModules(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<ICategoryService, UCCategoryService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }

        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            // AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }

        public static IServiceCollection AddValidators (this IServiceCollection services)
        {
              // Validators
            services.AddScoped<IValidator<CategoryDto>, CategoryDtoValidator>();

            return services;
        }
    }
}