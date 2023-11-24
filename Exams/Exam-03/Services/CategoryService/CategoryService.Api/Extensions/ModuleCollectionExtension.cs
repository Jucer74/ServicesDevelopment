using CategoryService.Application.Interfaces;
using CategoryService.Domain.Interfaces.Proxies;
using CategoryService.Domain.Interfaces.Repositories;
using CategoryService.Infrastructure.Proxies;
using CategoryService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CategoryService.Api.Extensions
{
    public static class ModuleCollectionExtension
    {
        public static IServiceCollection AddCoreModules(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<ICategoryService, CategoryService.Application.Services.CategoryService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddHttpClient<IProductProxy, ProductProxy>();

            return services;
        }
    }
}