using Pricat.Api;

var builder = WebApplication.CreateBuilder(args);

// Crear una instancia de Startup
var startup = new Startup(builder.Configuration);

// Llamar a ConfigureServices en Startup
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Llamar a Configure en Startup
startup.Configure(app, app.Environment);

app.Run();
