using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Interfaces;
using ProductService.Domain.Interfaces.Proxies;
using ProductService.Domain.Interfaces.Repositories;
using ProductService.Infrastructure.Proxies;
using ProductService.Infrastructure.Repositories;

namespace ProductService.Api.Extensions
{
    public static class ModuleCollectionExtension
    {
        public static IServiceCollection AddCoreModules(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<IProductService, ProductService.Application.Services.ProductService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddHttpClient<ICategoryProxy, CategoryProxy>();


            return services;
        }
    }
}