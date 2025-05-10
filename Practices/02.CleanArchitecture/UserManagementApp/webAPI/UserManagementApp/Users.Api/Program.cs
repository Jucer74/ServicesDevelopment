using Microsoft.EntityFrameworkCore;
using Users.Api.Extensions;
using Users.Api.Middlewares;
using Users.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("UserDb")));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Modules
builder.Services.AddCoreModules();
builder.Services.AddInfrastructureModules();
builder.Services.AddMapping();
builder.Services.AddValidators();
builder.WebHost.UseUrls("http://*:80");

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionMiddleware();

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