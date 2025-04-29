using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc; // Necesario para ApiBehaviorOptions
using Pricat.Api.Middleware;
using Pricat.Application.Interfaces;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Application.Mappering;
using Pricat.Application.Services;
using Pricat.Infrastructure.Contex;
using Pricat.Infrastructure.Repositoies;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("CnnStr"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("CnnStr"))
    )
);

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperCategory));

// Configurar manejo personalizado de errores de validación del modelo
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errorDetails = context.ConstructErrorMessages();
        return new BadRequestObjectResult(errorDetails);
    };
});

var app = builder.Build();

// Middleware para manejo de excepciones personalizadas
app.UseExceptionMiddleware();

// Middleware para manejo de errores globales
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var error = exceptionHandlerPathFeature?.Error;

        var errorDetails = new ErrorDetails
        {
            ErrorType = "Bad Request",
            Errors = new List<string> { error?.Message ?? "Invalid request." }
        };

        await context.Response.WriteAsync(errorDetails.ToString());
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
