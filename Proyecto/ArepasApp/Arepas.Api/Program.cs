using Arepas.Api.Extensions;
using Arepas.Api.Middleware;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices();
//builder.Services.AddMappinmg();
builder.Services.AddValidators();

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

app.Run();