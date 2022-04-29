using Microsoft.Extensions.DependencyInjection;
using ReminderApp.Application.Interfaces;
using ReminderApp.Application.Services;
using ReminderApp.Domain.Common;
using ReminderApp.Domain.Entities;
using ReminderApp.Domain.Interfaces.Repositories;
using ReminderApp.Infrastructure.Common;
using ReminderApp.Infrastructure.Repositories;

namespace ReminderApp.Api.Extensions
{
    public static class ModuleCollectionExtension
    {
        public static IServiceCollection AddCoreModules(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IReminderService, ReminderService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IReminderRepository, ReminderRepository>();

            return services;
        }
    }
}