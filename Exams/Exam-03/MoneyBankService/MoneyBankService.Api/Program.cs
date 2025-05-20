using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyBankService.Api.Extensions;
using MoneyBankService.Api.Middleware;
using MoneyBankService.Application.Validators;
using MoneyBankService.Application.Mappers;
using MoneyBankService.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add the DB Context
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("CnnStr")!));

// Add services to the container.
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        // Obtiene todos los errores del ModelState y los agrupa en una lista de cadenas.
        var errorDetails = context.ModelState
            .Where(kvp => kvp.Value != null && kvp.Value.Errors.Any())
            .SelectMany(kvp => kvp.Value!.Errors.Select(error => error.ErrorMessage))
            .ToList();


        // Convierte en una lista de cadenas

        // Opcional: Si quieres un diccionario con el nombre del campo y sus errores:
        /*
        var errorDetailsDictionary = context.ModelState
            .Where(modelState => modelState.Value.Errors.Any())
            .ToDictionary(
                modelState => modelState.Key, // El nombre del campo (ej. "Nombre", "Email")
                modelState => modelState.Value.Errors.Select(error => error.ErrorMessage).ToArray() // Los mensajes de error para ese campo
            );
        return new BadRequestObjectResult(errorDetailsDictionary);
        */

        return new BadRequestObjectResult(errorDetails); // Devuelve los errores como una lista de cadenas
    };
});

// Add Fluent Validation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// **Configuración de CORS - MODIFICADO**
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", // Cambié el nombre de la política para que sea más específico
        policy =>
        {
            policy.AllowAnyOrigin() // **Asegúrate de que este sea el puerto correcto de tu React**
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Add Modules
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<AccountValidator>();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMapping();
builder.Services.AddValidators();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add the Exception Middleware Handler
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();