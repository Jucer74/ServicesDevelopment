using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TeamsApi.Context;
using TeamsApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. conecta base de datos
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("CnnStr")!));

//Add Mvc options. agregar el fluid validator
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//servisios de extenciones los dos servicios.
builder.Services.AddServices();
builder.Services.AddMappinmg();

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