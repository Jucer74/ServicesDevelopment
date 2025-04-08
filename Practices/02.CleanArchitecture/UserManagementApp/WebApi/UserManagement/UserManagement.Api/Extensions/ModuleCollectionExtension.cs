using UserManagement.Aplication.Interfaces;
using UserManagement.Aplication.Services;
using UserManagement.Domain.Interfaces.Repositories;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Api.Extensions
{
    public static class ModuleCollectionExtension
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
