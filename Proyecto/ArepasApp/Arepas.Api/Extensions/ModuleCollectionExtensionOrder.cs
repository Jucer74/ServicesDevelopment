using Arepas.Application.Interfaces;
using Arepas.Application.Services;
using Arepas.Domain.Interfaces.Repositories;
using Arepas.Infrastructure.Repositories;

namespace Arepas.Api.Extensions
{
    public static class ModuleCollectionExtensionOrder
    {
        public static IServiceCollection AddCoreModulesOrder(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModulesOrder(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IOrderRepository, OrderRepository>();


            return services;
        }
    }
}
