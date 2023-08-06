using Newtonsoft.Json;

namespace STUDENTS
{
    public class Startup
    {
        // Aquí se inyecta la dependencia de IConfiguration
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Aquí se configura el servicio de NewtonSoftJson
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        // Aquí puedes agregar más configuraciones personalizadas de Newtonsoft.Json
                    });
            services.AddHttpClient();
        }

        // Aquí se configura el middleware de la aplicación
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
