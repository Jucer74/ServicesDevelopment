using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Services;
using UserManagement.Domain.Common;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces.Repositories;
using UserManagement.Infrastruct.Common;
using UserManagement.Infrastruct.Repositories;

namespace UserManagement.Api.Extensions
{
    public static class ModuleCollectionExtension
    {
        public static IServiceCollection AddCoreModules(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }
    }
}