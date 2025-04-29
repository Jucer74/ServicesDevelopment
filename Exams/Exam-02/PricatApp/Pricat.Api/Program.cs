using Microsoft.OpenApi.Models;
using Pricat.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pricat API", Version = "v1" });

    c.IgnoreObsoleteProperties();
    c.IgnoreObsoleteActions();
});
var app = builder.Build();

app.UseApiConfiguration(app.Environment);
app.MapControllers();

app.Run();