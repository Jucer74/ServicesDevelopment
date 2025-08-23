using UserManagement.Application.Interfaces;
using UserManagement.Application.Services;
using UserManagement.Domain.Interfaces.Repositories;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.api.Extensions
{
    public static class ModuleCollectionExtension
    {
        public static IServiceCollection AddCoreModules(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}