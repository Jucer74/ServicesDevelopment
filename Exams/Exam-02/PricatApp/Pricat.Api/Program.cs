using Pricat.Infrastructure.Persistence; 
using Microsoft.EntityFrameworkCore;     

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Pricat.Application.MappingProfiles.MappingProfile));

// Repositorios
builder.Services.AddScoped(typeof(Pricat.Application.Common.Interfaces.IRepository<>), typeof(Pricat.Application.Base.Repository<>));

// Servicios
builder.Services.AddScoped<Pricat.Application.Services.Interfaces.IProductService, Pricat.Application.Services.Implementation.ProductService>();
builder.Services.AddScoped<Pricat.Application.Services.Interfaces.ICategoryService, Pricat.Application.Services.Implementation.CategoryService>();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("CnnStr"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("CnnStr"))
    )
);

var app = builder.Build();

// Configurar middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
