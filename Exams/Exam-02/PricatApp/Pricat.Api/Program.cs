using FluentValidation.AspNetCore;
using Pricat.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pricat.Api.Extensions;
using Pricat.Api.Middleware;
using Pricat.Application.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("CnnStr"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("CnnStr"))
    ));

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errorDetails = context.ConstructErrorMessages();
            return new BadRequestObjectResult(errorDetails);
        };
    });

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom Extensions
builder.Services.AddApplicationModules();
builder.Services.AddInfrastructureModules();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddValidators();

var app = builder.Build();

// Configure the HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionMiddleware();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();