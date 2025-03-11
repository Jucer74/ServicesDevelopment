using Arepas.Api.Extensions;
using Arepas.Api.Middleware;
using Arepas.Infrastructure.Context;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DB Context
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("CnnStr")!));

builder.Services.AddControllers();

// Add Fluent Validation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Custom error handler for http BadRequest response
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

// Add services to the container.
builder.Services.AddValidators();
builder.Services.AddMappinmg();
builder.Services.AddApplicationRepositories();
builder.Services.AddApplicationServices();

// Add CORS
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add the Exception Middleware Handler
app.UseExceptionMiddleware();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => {
    options.WithOrigins("http://localhost:3000");
    options.WithOrigins("https://localhost:3001");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.WithExposedHeaders("X-Pagination");
});

app.Run();