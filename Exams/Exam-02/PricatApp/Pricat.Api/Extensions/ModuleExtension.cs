using Pricat.Application.Dtos;
using Pricat.Application.Interfaces.Services;
using Pricat.Application.Mapping;
using Pricat.Application.Services;
using AutoMapper;
using Pricat.Application.Interfaces.Repositories;
using FluentValidation;
using Pricat.Infrastructure.Repositories;
using Pricat.Application.Validations;

namespace Pricat.Api.Extensions;

public static class ModulesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
    {
        //repositories
        services.AddScoped<IProductRepository, ProductRepository>();
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
        services.AddScoped<IValidator<ProductDto>, ProductValidator>();
        services.AddScoped<IValidator<CategoryDto>, CategoryValidator>();

        return services;
    }
}