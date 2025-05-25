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

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


// Escuchar en todas las interfaces
builder.WebHost.UseUrls("http://*:80");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
