using PricatApp.Application.Interfaces;
using PricatApp.Application.Services;
using PricatApp.Domain.Interfaces.Repositories;
using PricatApp.Infrastructure.Repositories;

namespace PricatApp.Api.Extensions
{
    public static class ModuleCollectionExtensionCategorie
    {



        public static IServiceCollection AddCoreModulesCategorie(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<ICategorieService, CategorieService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModulesCategorie(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ICategorieRepository, CategorieRepository>();


            return services;
        }
    }
}