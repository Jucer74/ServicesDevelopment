using CategoryService.Api.Extensions;
using CategoryService.Api.Middleware;
using CategoryService.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CategoryService.Application.Interfaces;
using CategoryService.Application.Services;
using CategoryService.Domain.Interfaces.Repositories;
using CategoryService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add the DB Context
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("CnnStr")!));

// Add services to the container.
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errorDetails = context.ConstructErrorMessages();
        return new BadRequestObjectResult(errorDetails);
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICategoryService, CategoryServiceImpl>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddRestClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add the Exception Middleware Handler
app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
