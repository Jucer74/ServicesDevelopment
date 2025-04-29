using Microsoft.EntityFrameworkCore;
using System;
using Pricat.API.Extensions;
using Pricat.API.Middleware;
using Pricat.Infrastructure.Persistence.Context;
using Pricat.Application.Mappers;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Usamos nuestras extensiones para inyectar servicios
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper (si quieres usarlo, aunque todavía no lo configuramos)
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
