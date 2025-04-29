using Pricat.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Pricat.Domain.Interfaces; // Agrega este using
using Pricat.Infrastructure.Repositories; // Agrega este using
using Pricat.Application.Services;
using Pricat.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ��������������������������������
// Configurar DbContext MySQL
// ��������������������������������
var connectionString = builder.Configuration.GetConnectionString("CnnStr");
builder.Services.AddDbContext<PricatDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);
// ��������������������������������

// Registrar repositorio gen�rico
builder.Services.AddScoped(
    typeof(IRepository<>),
    typeof(BaseRepository<>)
);

// Registrar servicios de aplicaci�n
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();

// Registrar ILogger<ExceptionMiddleware> (A�ADE ESTA L�NEA)
builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

var app = builder.Build();
//Prueba de coneccion a base de dato
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<PricatDbContext>();

// Intenta realizar una operaci�n simple para forzar la conexi�n
try
{
    dbContext.Database.CanConnect();
    Console.WriteLine("Conexi�n a la base de datos exitosa durante el inicio.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error al conectar a la base de datos durante el inicio: {ex.Message}");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Registrar middleware de manejo de excepciones
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();



app.Run();
