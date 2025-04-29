using Microsoft.EntityFrameworkCore;
using Pricat.Infrastructure.Data;
using Pricat.Domain.Repositories;
using Pricat.Infrastructure.Repositories;
using Pricat.Application.Interfaces;
using Pricat.Application.Services;
using Pricat.Api.Middleware;    // <<<<<< Asegúrate de que coincida

var builder = WebApplication.CreateBuilder(args);

// Cadena de conexión
var conn = builder.Configuration.GetConnectionString("CnnStr");

// DbContext
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(conn, new MySqlServerVersion(new Version(8, 0, 28)))
);

// DI
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
