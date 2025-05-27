using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Application.Services;
using MoneyBankService.Domain.Interfaces.Repositories;
using MoneyBankService.Infrastructure.Context;
using MoneyBankService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1) Base de datos
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseMySQL(builder.Configuration.GetConnectionString("CnnStr")!));

// 2) Inyección de dependencias
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

// 3) Registrar Controllers y suprimir el filtro automático de validación de modelo
builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Configura la política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://moneybankweb-brianddia.s3-website.us-east-2.amazonaws.com") // <--- Aquí pon la URL de tu frontend. Agrega también la pública cuando lo subas.
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// 2. Usa la política de CORS
app.UseCors("AllowFrontend");
app.UseSwagger();
app.UseSwaggerUI();

// Escuchar en todas las interfaces
//builder.WebHost.UseUrls("http://*:80");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();