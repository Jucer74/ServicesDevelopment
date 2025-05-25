using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyBankService.Api.Extensions;
using MoneyBankService.Api.Middleware;
using MoneyBankService.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add the DB Context
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("CnnStr")!));

// Add services to the container.
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errorDetails = context.ConstructErrorMessages();
        return new BadRequestObjectResult(errorDetails);
    };
});
Console.WriteLine(builder.Configuration.GetConnectionString("CnnStr"));
// Add Fluent Validation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Modules
builder.Services.AddInfrastructureModules();
builder.Services.AddServices();
builder.Services.AddMapping();
builder.Services.AddValidators();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Cambia esto al origen de tu aplicación React
            .AllowAnyMethod() // Permitir todos los métodos HTTP (GET, POST, PUT, DELETE, etc.)
            .AllowAnyHeader(); // Permitir todos los encabezados
    });
});

// Configurar el servidor Kestrel para escuchar en los puertos 80 y 443
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // Puerto HTTP
    options.ListenAnyIP(443); // Puerto HTTPS
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MoneyBank API v1");
    c.RoutePrefix = "swagger"; // Deja esto para acceder vía /swagger
});


// Add the Exception Middleware Handler
app.UseExceptionMiddleware();

// app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();