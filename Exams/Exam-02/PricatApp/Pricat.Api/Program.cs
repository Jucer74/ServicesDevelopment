using MediatR;
using Pricat.Application.UseCases.GetCategories;
using Pricat.Domain.Interfaces;
using Pricat.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("ConnectionStrings"));
// 👇 REGISTRO DE DEPENDENCIAS
builder.Services.AddScoped<ICategoryRepository, InMemoryCategoryRepository>();
builder.Services.AddMediatR(typeof(GetCategoriesQueryHandler).Assembly);
builder.Services.AddScoped<IProductRepository, InMemoryProductRepository>();
var app = builder.Build();

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
