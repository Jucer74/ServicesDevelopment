using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Students.Api.Extensions;
using Students.Application.Interfaces;
using Students.Application.Services;
using Students.Infrastructure.Context;

namespace Students.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("StudentsDB")));

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Students.Api", Version = "v1" });
        });


        // Add Modules
        services.AddCoreModules();
        services.AddInfrastructureModules();


        services.AddCors();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Students.Api v1"); });
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseCors(options =>
        {
            options.WithOrigins("http://localhost:3000");
            options.AllowAnyMethod();
            options.AllowAnyHeader();
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}