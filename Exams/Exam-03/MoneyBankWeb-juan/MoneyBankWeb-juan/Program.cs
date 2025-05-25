var builder = WebApplication.CreateBuilder(args);

// Define el nombre de la pol�tica CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Agrega servicios al contenedor.
builder.Services.AddRazorPages();

// Configura la pol�tica CORS para permitir tu frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7188") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configura el pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

// Usa la pol�tica CORS antes de Authorization
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapRazorPages();

app.Run();
