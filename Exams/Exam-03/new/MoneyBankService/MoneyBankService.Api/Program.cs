using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Application.Services;
using MoneyBankService.Domain.Interfaces.Repositories;
using MoneyBankService.Infrastructure.Context;
using MoneyBankService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la conexión a la base de datos
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("CnnStr")!));

// 2. Registrar repositorio y servicio
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

// 2. Suprimir filtro automático
builder.Services.AddControllers(options =>
{
    // Reemplazar SuppressModelStateInvalidFilter con SuppressImplicitRequiredAttributeForNonNullableReferenceTypes
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();