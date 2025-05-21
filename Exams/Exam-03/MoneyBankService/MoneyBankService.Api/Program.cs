using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyBankService.Api.Extensions;
using MoneyBankService.Api.Middleware;
using MoneyBankService.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add the DB Context
// Add DBContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("CnnStr"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("CnnStr"))
    )
);
// Add services to the container.
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errorDetails = context.ConstructErrorMessages();
        return new BadRequestObjectResult(errorDetails);
    };
});

// Add Fluent Validation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Modules
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMapping();
builder.Services.AddValidators();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Add the Exception Middleware Handler
app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();