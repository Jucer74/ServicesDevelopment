using Microsoft.EntityFrameworkCore;
using MoneyBankService.Infrastructure.Context;
using MoneyBankService.Api.Extensions;
using MoneyBankService.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configuración del contexto con MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("CnnStr")!));

// Servicios del dominio y aplicación
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddMapping();
builder.Services.AddValidators();

// Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware global de excepciones
app.UseExceptionMiddleware();

// Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
