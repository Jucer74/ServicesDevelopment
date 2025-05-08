using System.Reflection;
using Microsoft.OpenApi.Models;
using Pricat.Utilities.Extensions;

namespace Pricat.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Pricat API",
                    Version = "v1",
                    Description = "API para gestión de productos y categorías",
                    Contact = new OpenApiContact { Name = "Equipo Pricat", Email = "contacto@pricat.com" }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            services.AddInfrastructure(configuration);

            return services;
        }
    }
}