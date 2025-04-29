using AutoMapper;
using FluentValidation;
using Pricat.Application.Dtos;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Application.Interfaces.Services;
using Pricat.Application.Mapping;
using Pricat.Application.Services;
using Pricat.Application.Validations;
using Pricat.Infrastructure.Repositories;

namespace Pricat.Api.Extensions;

public static class ModulesExtension
{
    public static IServiceCollection AddApplicationModules(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        return services;
    }

    public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        services.AddSingleton(mapperConfig.CreateMapper());
        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CategoryDto>, CategoryValidator>();
        services.AddScoped<IValidator<ProductDto>, ProductValidator>();
        return services;
    }
}