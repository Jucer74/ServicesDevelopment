using Pricat.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Pricat.Application.Validations;
using Pricat.Api.Middlewares;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Custom ModelState Error Handling
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errorDetails = context.ConstructErrorMessages();
            var result = new BadRequestObjectResult(errorDetails);
            result.ContentTypes.Add("application/json");
            return result;
        };
    });

// FluentValidation (simplificado)
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Pricat.Application.MappingProfiles.MappingProfile));

// Repositorios
builder.Services.AddScoped(typeof(Pricat.Application.Common.Interfaces.IRepository<>), typeof(Pricat.Application.Base.Repository<>));

// Servicios
builder.Services.AddScoped<Pricat.Application.Services.Interfaces.IProductService, Pricat.Application.Services.Implementation.ProductService>();
builder.Services.AddScoped<Pricat.Application.Services.Interfaces.ICategoryService, Pricat.Application.Services.Implementation.CategoryService>();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("CnnStr"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("CnnStr"))
    )
);

var app = builder.Build();


// Middleware global
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseExceptionMiddleware();
app.UseAuthorization();
app.MapControllers();
app.Run();
