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

// 2) Inyecci�n de dependencias
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

// 3) Registrar Controllers y suprimir el filtro autom�tico de validaci�n de modelo
builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Configura la pol�tica de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://moneybankweb-brianddia.s3-website.us-east-2.amazonaws.com") // <--- Aqu� pon la URL de tu frontend. Agrega tambi�n la p�blica cuando lo subas.
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// 2. Usa la pol�tica de CORS
app.UseCors("AllowFrontend");
app.UseSwagger();
app.UseSwaggerUI();

// Escuchar en todas las interfaces
//builder.WebHost.UseUrls("http://*:80");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();