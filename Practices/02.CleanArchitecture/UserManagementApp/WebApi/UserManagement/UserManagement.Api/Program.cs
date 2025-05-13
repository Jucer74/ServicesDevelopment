using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Middlewares;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Services;
using UserManagement.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración del DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registra el servicio
builder.Services.AddScoped<IPersonService, PersonService>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


// Escuchar en todas las interfaces
app.Urls.Add("http://0.0.0.0:80");

// Añadir el middleware de excepciones
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
