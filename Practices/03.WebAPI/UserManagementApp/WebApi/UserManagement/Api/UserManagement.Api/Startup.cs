using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserManagement.Api.Middleware;
using UserManagement.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using UserManagement.Api.Extensions;
using UserManagement.Application.Common;
using UserManagement.Infrastructure.Common;

namespace UserManagement.Api;
public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configurar DbContext with MySql
        //CnnStr: "server=localhost;port=3306;database=usersdb;user=usersuser;password=Us3rsUs3r*01"
        /*services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                Configuration.GetConnectionString("CnnStr")!,
                ServerVersion.AutoDetect(Configuration.GetConnectionString("CnnStr")!)
            )
        );*/
        
        // Configurar DbContext with SQL Lite
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("CnnStr")));
        
        // Registrar servicios genéricos
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        // Agregar controladores
        services.AddControllers();
        
        // Configurar Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserManagement API", Version = "v1" });
        });
        
        // Registrar módulos personalizados
        services.AddCoreModules();
        services.AddInfrastructureModules();
        // Configurar CORS
        services.AddCors();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment() || Configuration.GetValue<bool>("ENABLE_SWAGGER"))
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagement API v1"));
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