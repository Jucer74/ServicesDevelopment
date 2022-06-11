using Microsoft.Extensions.DependencyInjection;
using Pricat.Application.Interfaces;
using Pricat.Application.Services;
using Pricat.Domain.Interfaces.Repositories;
using Pricat.Infrastructure.Repositories;

namespace Pricat.Api.Extensions
{
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
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();


            return services;
        }
    }
}
