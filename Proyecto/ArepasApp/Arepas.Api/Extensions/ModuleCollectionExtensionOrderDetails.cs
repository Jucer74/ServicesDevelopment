using Arepas.Application.Interfaces;
using Arepas.Application.Services;
using Arepas.Domain.Interfaces.Repositories;
using Arepas.Infrastructure.Repositories;

namespace Arepas.Api.Extensions
{
    public static class ModuleCollectionExtensionOrderDetails
    {
        public static IServiceCollection AddCoreModulesOrderDetails(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<IOrderDetailService, OrderDetailService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModulesOrderDetails(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IOrderDetailRepository, OrderDetailsRepository>();


            return services;
        }
    }
}