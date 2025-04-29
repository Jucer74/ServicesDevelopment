<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Infractructure.Persistence.Context;
using UserManagement.Infractructure.Repositories;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
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
<<<<<<< HEAD
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

=======
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

<<<<<<< HEAD
// Add the Exception Middleware Handler
app.UseExceptionMiddleware();

=======
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
