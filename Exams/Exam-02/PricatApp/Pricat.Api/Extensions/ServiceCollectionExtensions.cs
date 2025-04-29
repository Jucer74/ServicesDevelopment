// En el archivo ModuleCollectionExtension.cs
using Microsoft.Extensions.DependencyInjection;
using Pricat.Application.Interfaces;
using Pricat.Application.Services;
using Pricat.Infrastructure.Persistence.Repositories;
using Pricat.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Pricat.Domain.Entities;

namespace Pricat.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Servicios de aplicación
            services.AddScoped<ICategoryService, CategoryService>();
            // Agregar más servicios aquí después
            services.AddScoped<IProductService, ProductService>();
            return services;
        }

        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración de DbContext
            services.AddDbContext<PricatDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("CnnStr"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("CnnStr"))
                ));

            // Repositorios
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Agregar más repositorios aquí después
            return services;
        }
    }
}
