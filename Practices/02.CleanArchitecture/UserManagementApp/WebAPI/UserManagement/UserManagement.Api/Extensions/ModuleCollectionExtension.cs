using AutoMapper;
using FluentValidation;

using UserManagement.App.Mapping;
using UserManagement.App.Dtos;
using UserManagement.App.Interfaces;
using UserManagement.App.Services;
using UserManagement.App.Validations;
using UserManagement.Dom.Interfaces.Repositories;
using UserManagement.Infrastucture.Repositories;

namespace UserManagement.Api.Extensions
{
    public static class ModuleCollectionExtension
    {
        public static IServiceCollection AddCoreModules(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<IUserServices, UserServices>();

            // Validators
            services.AddScoped<IValidator<UserDto>, UserValidator>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddMappingModules(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}