using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Extensions;
using UserManagement.App.Interfaces;
using UserManagement.App.Services;
using UserManagement.Infrastucture.Context;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Asegúrate de que se registre IUserServices en el contenedor de dependencias en Program.cs
builder.Services.AddScoped<IUserServices, UserServices>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Modules
builder.Services.AddCoreModules();
builder.Services.AddInfrastructureModules();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDeveloperExceptionPage();  

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();

app.UseAuthorization();




app.MapControllers();

app.Run();