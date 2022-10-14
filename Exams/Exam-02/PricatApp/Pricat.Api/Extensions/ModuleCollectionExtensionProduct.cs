using PricatApp.Application.Interfaces;
using PricatApp.Application.Services;
using PricatApp.Domain.Interfaces.Repositories;
using PricatApp.Infrastructure.Repositories;

namespace PricatApp.Api.Extensions
{
    public static class ModuleCollectionExtensionProduct
    {



        public static IServiceCollection AddCoreModulesProduct(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<IProductService, ProductService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModulesProduct(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IProductRepository, ProductRepository>();


            return services;
        }
    }
}