using Microsoft.Extensions.DependencyInjection;
using Pricat.Application.interfaces;
using Pricat.Application.services;
using Pricat.Domain.interfaces.Repositories;

using Pricat.Infrastructure.repositories;


namespace Pricat.Api.Extensions
{
    public static class ModuleCollectionExtension
    {
        public static IServiceCollection AddCoreModules(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IProductsService, ProductsService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IproductsRepository, ProductsRepository>();


            return services;
        }
    }
}
