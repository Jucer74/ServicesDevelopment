using Students.Application.Interfaces;
using Students.Application.Services;

namespace Students.Api.Extensions;

public static class ModuleCollectionExtension
{
    public static IServiceCollection AddCoreModules(this IServiceCollection services)
    {
        // Services / Use Cases
        services.AddScoped<IStudentService, StudentService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IStudentService, StudentService>();

        return services;
    }
}