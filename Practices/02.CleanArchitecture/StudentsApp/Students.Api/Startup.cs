using Microsoft.OpenApi.Models;
using Students.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Students.Api.;


namespace Students.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AppDbContext>(options => options.UseSqlite("Name=StudentsDB"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Students.Api", Version = "v1" });
            });

            // Add Modules
            services.AddCoreModules();
            services.AddInfrastructureModules();

            services.AddCors();
        }
    }
}