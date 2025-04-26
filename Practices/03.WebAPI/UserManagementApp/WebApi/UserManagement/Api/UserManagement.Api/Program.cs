using UserManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Extensions;
using UserManagement.Application.Common;
using UserManagement.Infrastructure.Common;

var builder = WebApplication.CreateBuilder(args);

//Add DbContext

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("CnnStr")!,
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("CnnStr")!)
    )
);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Modules
builder.Services.AddCoreModules();
builder.Services.AddInfrastructureModules();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseCors(options => {
    options.WithOrigins("http://localhost:3000");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.MapControllers();

app.Run();