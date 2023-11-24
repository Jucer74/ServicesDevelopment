using AutoMapper;
using FluentValidation;
using RestSharp;
using CategoryService.Api.Dtos;
using CategoryService.Api.Mapping;
using CategoryService.Api.Validators;
using CategoryService.Application.Interfaces;
using CategoryService.Domain.Interfaces.Repositories;
using CategoryService.Infrastructure.Repositories;
using Categoryservice.Application.Services;

namespace CategoryService.Api.Extensions;

   public static class ModulesExtension
   {
      public static IServiceCollection AddServices(this IServiceCollection services)
      {
        // Services / Use Cases
        services.AddScoped<ICategoryService, CategoryServic>();
         

         return services;
      }

      public static IServiceCollection AddRepositories(this IServiceCollection services)
      {
         // Repositories
         services.AddScoped<ICategoryRepository, CategoryRepository>();
         

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

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CategoryDto>, CategoryValidator>();

        return services;
    }

    public static IServiceCollection AddRestClient(this IServiceCollection services)
    {
        services.AddSingleton<RestClient>();

        return services;
    }
}
