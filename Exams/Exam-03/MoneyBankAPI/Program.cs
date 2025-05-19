using Microsoft.EntityFrameworkCore;
using MoneyBank.Application.Interfaces;
using MoneyBank.Application.Services;
using MoneyBank.Domain.Interfaces;
using MoneyBank.Infrastructure.Data;
using MoneyBank.Infrastructure.Repositories;
using MoneyBankAPI.Context;

var builder = WebApplication.CreateBuilder(args);

// Inyectar DbContext (InMemory)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("MoneyBankDb"));

// Inyectar repositorio e interfaces de servicio
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();


// Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
