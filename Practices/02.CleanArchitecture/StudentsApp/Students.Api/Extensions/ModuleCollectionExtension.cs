using Students.Application.Interfaces;
using Students.Application.Services;
using Students.Domain.Interfaces.Repositories;
using Students.Infrastructure.Repositories;

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
        services.AddScoped<IStudentRepository, StudentRepository>();

        return services;
    }
}