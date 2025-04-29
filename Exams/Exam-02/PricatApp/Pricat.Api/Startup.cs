using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pricat.Api.Extensions;
using Pricat.Api.Filters;
using Pricat.Api.Middleware;
using Pricat.Application.Common;
using Pricat.Infrastructure.Common;
using Pricat.Infrastructure.Context;

namespace Pricat.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        // Configurar DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                Configuration.GetConnectionString("CnnStr")!,
                ServerVersion.AutoDetect(Configuration.GetConnectionString("CnnStr")!)
            )
        );
        
        // Registrar servicios genéricos
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        // Agregar controladores
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });
        
        // Configurar Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserManagement API", Version = "v1.0" });
        });
        
        // Registrar módulos personalizados
        services.AddCoreModules();
        services.AddInfrastructureModules();
        // Configurar CORS
        services.AddCors();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PricatApp API v1.0"));
        }
        
        // Add the Exception Middleware Handler
        app.UseExceptionMiddleware();
        
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseCors("AllowSpecificOrigin");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}