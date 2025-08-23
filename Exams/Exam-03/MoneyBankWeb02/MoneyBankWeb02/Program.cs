using MoneyBankWeb02.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Agregar soporte para controllers con vistas (MVC)
builder.Services.AddControllersWithViews();

// Registrar HttpClient para la API usando un cliente nombrado "MoneyBankApi"
builder.Services.AddHttpClient("MoneyBankApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        // Ignora la validación del certificado (solo en desarrollo)
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});

// Registrar el servicio que consume la API
builder.Services.AddScoped<AccountService>();

var app = builder.Build();

// Configuración del pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Ruta predeterminada para que la raíz redirija a Accounts/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=Index}/{id?}"
);

app.Run();
