using Pricat.Infrastructure.Context;
using Pricat.Api.Middleware;
using Microsoft.EntityFrameworkCore;
using Pricat.Api.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//AppDBContext
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseMySql(
      builder.Configuration.GetConnectionString("CnnStr"),
      ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("CnnStr"))
   ));

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
   options.InvalidModelStateResponseFactory = context =>
   {
      var errorDetails = context.ConstructErrorMessages();
      return new BadRequestObjectResult(errorDetails);
   };
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddServices();
builder.Services.AddMapping();
builder.Services.AddInfrastructureModules();
builder.Services.AddValidators();

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
