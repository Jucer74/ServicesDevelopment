using Pricat.Application.Interfaces;
using Pricat.Application.Services;

namespace Pricat.Api.Extensions;

public static class ModuleCollectionExtension
{
    public static IServiceCollection AddCoreModules(this IServiceCollection services)
    {
        // Services / Use Cases
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
    
    public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}