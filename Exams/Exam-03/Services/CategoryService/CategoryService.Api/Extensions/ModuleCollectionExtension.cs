using CategoryService.Application.Interfaces;
using CategoryService.Application.Services;
using CategoryService.Domain.Interfaces.Repositories;
using RestSharp;
using CategoryService.Infrastructure.Repositories;

namespace CategoryService.Api.Extensions
{
   public static class ModuleCollectionExtension
   {
        public static IServiceCollection AddCoreModules(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<ICategoryService, CategoryServiceImpl>();
            return services;
        }

        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }

        public static IServiceCollection AddRestClient(this IServiceCollection services)
        {
            services.AddSingleton<RestClient>();
            return services;
        }
    }
}