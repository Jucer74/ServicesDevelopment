using Arepas.Application.Interfaces;
using Arepas.Application.Services;
using Arepas.Domain.Interfaces.Repositories;
using Arepas.Infrastructure.Repositories;

namespace Arepas.Api.Extensions
{
    public static class ModuleCollectionExtensionProduct
    {
        public static IServiceCollection AddCoreModulesProduct(this IServiceCollection services)
        {
            // Services / Use Cases
            //services.AddScoped<Arepas.Application.Interfaces.IProductService,ProductService>();
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
