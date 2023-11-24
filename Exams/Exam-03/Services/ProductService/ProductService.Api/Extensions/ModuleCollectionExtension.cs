
using AutoMapper;
using ProductService.Api.Mapping;
using ProductServiceAPI.Domain.Interfaces.Repositories;
using ProductServiceAPI.Infrastructure.Repositories;
using RestSharp;

namespace ProductServiceAPI.Api.Extensions
{
   public static class ModuleCollectionExtension
   {
      public static IServiceCollection AddCoreModules(this IServiceCollection services)
      {
         // Services / Use Cases
         services.AddScoped<IProductRepository, ProductRepository>();
         return services;
      }

      public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
      {
         services.AddScoped<IProductRepository, ProductRepository>();
         return services;
      }

       public static IServiceCollection AddRestClient(this IServiceCollection services)
       {
           services.AddSingleton<RestClient>();
           return services;
       }
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductService.Api.Mapping.Mapper());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}