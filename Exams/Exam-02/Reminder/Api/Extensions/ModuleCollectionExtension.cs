using Microsoft.Extensions.DependencyInjection;
using ReminderAPP.Application.Interfaces;
using ReminderAPP.Application.Services;
using ReminderAPP.Domain.Interface.Repositories;
using ReminderAPP.Infrastructure.Repositories;


namespace ReminderAPP.Api.Extensions
{
    public static class ModuleCollectionExtension
    {
        public static IServiceCollection AddCoreModules(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IReminderService, ReminderService>();

            return services;
        }
        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IReminderRepository, ReminderRepository>();

            return services;

        }


    }
}
