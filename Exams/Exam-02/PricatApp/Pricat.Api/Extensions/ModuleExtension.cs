// Importación de librerías necesarias
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

// Clase estática que define métodos de extensión para registrar módulos en el contenedor de servicios
public static class ModulesExtension
{
    // Registra los servicios de aplicación (servicios de negocio) en el contenedor
    public static IServiceCollection AddApplicationModules(this IServiceCollection services)
    {
        // Asocia las interfaces de servicios con sus implementaciones
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        return services;
    }

    // Registra los módulos de infraestructura (repositorios) en el contenedor
    public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
    {
        // Asocia las interfaces de repositorios con sus clases concretas
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }

    // Configura AutoMapper con el perfil de mapeo definido en la aplicación
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        // Crea una configuración de AutoMapper usando el perfil personalizado
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile ()); // Se registra el perfil de mapeo
        });

        // Agrega una instancia singleton del IMapper generado
        services.AddSingleton(mapperConfig.CreateMapper());
        return services;
    }

    // Registra los validadores de FluentValidation para los DTOs
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        // Asocia cada DTO con su clase validadora correspondiente
        services.AddScoped<IValidator<CategoryDto>, CategoryValidator>();
        services.AddScoped<IValidator<ProductDto>, ProductValidator>();
        return services;
    }
}
