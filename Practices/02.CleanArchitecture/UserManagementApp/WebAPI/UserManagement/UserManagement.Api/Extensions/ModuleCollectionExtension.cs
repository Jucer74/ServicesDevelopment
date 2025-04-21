using UserManagement.App.Interfaces;
using UserManagement.App.Services;
using UserManagement.Dom.Interfaces.Repositories;
using UserManagement.Infrastucture.Repositories;


namespace UserManagement.Api.Extensions
{
    public static class ModuleCollectionExtension
    {
        public static IServiceCollection AddCoreModules(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<IUserServices, UserServices>();

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