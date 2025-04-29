using Pricat.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ————————————————————————————————
// Configurar DbContext MySQL
// ————————————————————————————————
var connectionString = builder.Configuration.GetConnectionString("CnnStr");
builder.Services.AddDbContext<PricatDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);
// ————————————————————————————————

var app = builder.Build();
// Prueba de coneccion a base de dato
//using var scope = app.Services.CreateScope();
//var dbContext = scope.ServiceProvider.GetRequiredService<PricatDbContext>();

//// Intenta realizar una operación simple para forzar la conexión
//try
//{
//    dbContext.Database.CanConnect();
//    Console.WriteLine("Conexión a la base de datos exitosa durante el inicio.");
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Error al conectar a la base de datos durante el inicio: {ex.Message}");
//}

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
