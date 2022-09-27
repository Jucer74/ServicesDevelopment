using EmployeeApp.Application.Interfaces;
using EmployeeApp.Application.Services;
using EmployeeApp.Domain.Interfaces.Repositories;
using EmployeeApp.Infrastructure.Repositories;

namespace EmployeeApp.Api.Extensions
{
    public static class ModuleCollectionExtension
    {
        public static IServiceCollection AddCoreModules(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();


            return services;
        }
    }
}