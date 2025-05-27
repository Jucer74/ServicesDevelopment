using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyBankService.Api.Extensions;
using MoneyBankService.Api.Middleware;
using MoneyBankService.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("CnnStr")!));

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errorDetails = context.ConstructErrorMessages();
        return new BadRequestObjectResult(errorDetails);
    };
});

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Swagger siempre habilitado
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMapping();
builder.Services.AddValidators();

var app = builder.Build();

// HABILITAR SWAGGER EN TODOS LOS ENTORNOS
app.UseSwagger();
app.UseSwaggerUI();

// ELIMINAR HTTPS REDIRECTION PARA DOCKER
// app.UseHttpsRedirection(); // ⚠️ Esto rompe en contenedor Docker si no hay puerto 443

app.UseExceptionMiddleware();

app.UseAuthorization();

app.MapControllers();

// Redirigir root a Swagger (opcional)
app.MapGet("/", () => Results.Redirect("/swagger"));

// Hacer que escuche en el puerto 80 (ya lo haces bien)
//app.Run("http://*:80");
app.Run();