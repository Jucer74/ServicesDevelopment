using Arepas.Application.Interfaces;
using Arepas.Application.Services;
using Arepas.Domain.Interfaces.Repositories;
using Arepas.Infrastructure.Repositories;

namespace Arepas.Api.Extensions
{
    public static class ModuleCollectionExtensionCustomer
    {
        public static IServiceCollection AddCoreModulesCustomer(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModulesCustomer(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();


            return services;
        }
    }
}
