<<<<<<< HEAD:Practices/02.WebAPI/TeamsApi/Program.cs
=======
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamsApi.Context;
using TeamsApi.Extensions;
using TeamsApi.Middleware;

>>>>>>> main:Practices/03.WebAPI/TeamsApi/Program.cs
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
<<<<<<<< HEAD:object value = Practices /02.CleanArchitecture/StudentsApp/StudentsApp.Api/Program.cs;
========
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("CnnStr")!));

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errorDetails = context.ConstructErrorMessages();
        return new BadRequestObjectResult(errorDetails);
    };
});

//Add Mvc options
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
>>>>>>>> main:Practices/02.WebAPI/TeamsApi/Program.cs

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

<<<<<<< HEAD:Practices/02.WebAPI/TeamsApi/Program.cs
=======
builder.Services.AddServices();
builder.Services.AddMapping();
builder.Services.AddValidators();

>>>>>>> main:Practices/03.WebAPI/TeamsApi/Program.cs
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add the Exception Middleware Handler
app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
