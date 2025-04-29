using AutoMapper;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Application.Interfaces.Services;
using Pricat.Application.Mapping;
using Pricat.Application.Services;
using Pricat.Infrastructure.Repositories;

namespace Pricat.Api.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }

         public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
         {
            //repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
         }

        public static IServiceCollection AddMapping(this IServiceCollection services)
        {

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
        //public static IServiceCollection AddValidators(this IServiceCollection services)
        //{
        //    services.AddScoped<IValidator<CategoryDto>, TeamValidator>();
        //    services.AddScoped<IValidator<TeamMemberDto>, TeamMemberValidator>();

        //    return services;
        //}
    }
}
