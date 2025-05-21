using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using UserManagement.Infrastruct.Context;
using UserManagement.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Leer la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configurar DbContext para usar MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29))));

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar módulos (Services y Repositories)
builder.Services.AddCoreModules();
builder.Services.AddInfrastructureModules();

builder.Services.AddCors();

var app = builder.Build();

// Agrega esto para mostrar Swagger en producción también
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API V1");
    c.RoutePrefix = "swagger"; // para que funcione en /swagger
});



app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseCors(options => {
    options.WithOrigins("http://localhost:3000");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.MapControllers();

app.Run();
