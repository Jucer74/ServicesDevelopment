using Microsoft.EntityFrameworkCore;
using Netbank.Application.Interfaces;
using Netbank.Application.Services;
using NetBank.Infrastructure.Context;
using NetBank.Infrastructure.Repositories;
using Pricat.Domain.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("CnnStr")!));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionar os Servicios
builder.Services.AddScoped<IIssuingNetworkRepository, IssuingNetworkRepository>();
builder.Services.AddScoped<ICreditCardService, CreditCardService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();