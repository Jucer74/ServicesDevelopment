using Users.Application.Interfaces;
using Users.Application.Services;
using Users.Domain.Interfaces.Repositories;
using Users.Infrastructure.Repositories;

namespace Users.Api.Extensions
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