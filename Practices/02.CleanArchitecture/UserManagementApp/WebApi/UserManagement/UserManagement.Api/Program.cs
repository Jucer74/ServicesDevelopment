using Microsoft.EntityFrameworkCore;
using UserManagement.Infrastr.Context;
using UserManagement.Api.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


// Add services to the container
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add custom modules
builder.Services.AddUserManagementModule();
builder.Services.AddInfrastructureRepositories();

builder.Services.AddCors();

var app = builder.Build();

// HTTP request pipeline config
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.MapControllers();

app.Run();
