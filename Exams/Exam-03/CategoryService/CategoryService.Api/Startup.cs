using CategoryService.Api.Extensions;
using CategoryService.Api.Middleware;
using CategoryService.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;

namespace CategoryService.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Configuration.GetConnectionString("CnnStr");
        services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString!));

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pricat.Api", Version = "v1" });
        });

        // Custom error handler for http BadRequest response
        services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
         {
             var errorDetails = context.ConstructErrorMessages();
             return new BadRequestObjectResult(errorDetails);
         };
        });

        // Add Fluent Validation
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        // Add Modules
        services.AddCoreModules();
        services.AddInfrastructureModules();
        services.AddMapping();
        services.AddValidators();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pricat.Api v1"));
        }

        // Add the Exception Middleware Handler
        app.UseExceptionMiddleware();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}