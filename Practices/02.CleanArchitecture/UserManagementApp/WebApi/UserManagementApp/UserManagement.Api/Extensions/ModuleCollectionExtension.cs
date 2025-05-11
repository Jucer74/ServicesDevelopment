using UserManagement.Application.Interfaces;
using UserManagement.Application.Services;
using UserManagement.Domain.Interfaces.Repositores;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Api.Extensions
{
    public static class ModuleCollectionExtension
    {
        public static IServiceCollection AddCoreModules(this IServiceCollection services)
        {
            // Services / Use cases
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
