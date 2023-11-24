using AutoMapper;
using CategoryService.Api.Dtos;
using CategoryService.Domain.Interfaces.Repositories;
using CategoryService.Infrastructure.Repositories;
using FluentValidation;
using CategoryService.Api.Dtos;
using CategoryService.Api.Mapping;
using CategoryService.Api.Validators;
using CategoryService.Application.Interfaces;
using CategoryService.Application.Services;
using CategoryService.Domain.Interfaces.Repositories;
using CategoryService.Infrastructure.Repositories;

namespace CategoryService.Api.Extensions;

public static class ModulesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, SCategoryService>();
        
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }


    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        // Auto Mapper Configurations
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CategoryDto>, CategoryValidator>();

        return services;
    }
}