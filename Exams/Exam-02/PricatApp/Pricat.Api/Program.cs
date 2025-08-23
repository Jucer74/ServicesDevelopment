using Microsoft.EntityFrameworkCore;
using Pricat.Application.Interfaces;
using Pricat.Application.Services;
using Pricat.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Leer la cadena de conexión desde el archivo de configuración
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La cadena de conexión no se ha configurado correctamente.");
}

// Configuración de DbContext para usar MySQL
builder.Services.AddDbContext<PricatDbContext>(options =>
    options.UseMySql(connectionString));

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
