using Product.Api.Mapping;
using Product.Application.Interfaces;
using Product.Application.Services;
using Product.Domain.Interfaces.Repositories;
using Product.Infrastructure.Repositories;
using AutoMapper;
using FluentValidation;
using Product.Api.Dtos;
using Product.Api.Validators;

namespace Product.Api.Extensions;

public static class ModuleCollectionExtension
{
    public static IServiceCollection AddCoreModules(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }

    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<ProductDto>, ProductValidator>();

        return services;
    }
}