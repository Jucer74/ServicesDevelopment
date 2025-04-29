
using UserManagement.Domain.Interfaces.Repositories;
using UserManagement.App.Interfaces;
using UserManagement.Application.Services;
using UserManagement.Infrastr.Repositories;

namespace UserManagement.Api.Extensions;

public static class ModuleCollectionExtension
{
    public static IServiceCollection AddUserManagementModule(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }
    public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
