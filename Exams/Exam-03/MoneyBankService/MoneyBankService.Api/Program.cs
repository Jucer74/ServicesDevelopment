using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyBankService.Api.Extensions;
using MoneyBankService.Api.Middleware;
using System.Text.Json; // Asegúrate de que este using esté presente
using MoneyBankService.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add the DB Context
var connectionString = builder.Configuration.GetConnectionString("CnnStr"); // Obtén la cadena de conexión
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)) // Sintaxis correcta para Pomelo
);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options => 
    {
        
       // options.JsonSerializerOptions.PropertyNamingPolicy = null;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errorDetails = context.ConstructErrorMessages();
            return new BadRequestObjectResult(errorDetails);
        };
    });

// Add Fluent Validation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Modules
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMapping();
builder.Services.AddValidators();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add the Exception Middleware Handler
app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();