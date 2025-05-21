using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneyBankService.API.Middleware; // Asegúrate de que este namespace sea correcto
using MoneyBankService.Application.Interfaces; // Para IAccountService
using MoneyBankService.Application.Interfaces.Repositories; // Para IAccountRepository
using MoneyBankService.Application.Services; // Para AccountService

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configuración de la inyección de dependencias

// 1. Registrar tu AccountService
builder.Services.AddScoped<IAccountService, AccountService>();

// 2. Registrar tu IAccountRepository y su implementación
// Si usas un repositorio en memoria para pruebas o desarrollo:
builder.Services.AddSingleton<IAccountRepository, InMemoryAccountRepository>();
// Si usas Entity Framework Core, la configuración sería diferente, por ejemplo:
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// builder.Services.AddScoped<IAccountRepository, EfCoreAccountRepository>(); // Tu implementación de repositorio EF Core

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

// ¡IMPORTANTE! Coloca el middleware de manejo de errores al principio del pipeline.
// Esto asegura que capture todas las excepciones lanzadas por tus controladores y servicios.
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();