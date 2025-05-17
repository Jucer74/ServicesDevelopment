<<<<<<< HEAD
<<<<<<< HEAD
=======
using System;
>>>>>>> 219361b297b922b7a9e1dd565c70121e55f718f4
using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Middleware;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Infrastructure.Persistence.Context;
using UserManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
=======
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

<<<<<<< HEAD
app.UseExceptionMiddleware();

=======
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

<<<<<<< HEAD
app.Run();
=======
app.Run();
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
