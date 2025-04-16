using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Services;

namespace UserManagement.Api.Extensions;

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
        //Repositories
        services.AddScoped<IUserService, UserService>();
        return services;
    }


}