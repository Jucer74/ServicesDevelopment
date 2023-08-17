using StudentsApp.Application.Interfaces;
using StudentsApp.Application.Services;
using StudentsApp.Domain.Interfaces.Repositories;
using StudentsApp.Infrastructure.Repositories;

namespace StudentsApp.Api.Extensions;

public static class ModuleCollectionExtensions
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