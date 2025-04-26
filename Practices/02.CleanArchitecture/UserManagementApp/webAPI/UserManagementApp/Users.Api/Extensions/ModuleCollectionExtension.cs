using AutoMapper;
using FluentValidation;
using Users.Application.Dtos;
using Users.Application.Interfaces;
using Users.Application.Interfaces.Repositories;
using Users.Application.Mapping;
using Users.Application.Services;
using Users.Application.Validations;
using Users.Infrastructure.Repositories;
using System.Linq.Expressions;

namespace Users.Api.Extensions
{
    public static class ModuleCollectionExtension
    {
        public static IServiceCollection AddCoreModules(this IServiceCollection services)
        {
            // Services / Use Cases
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingUserProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserDto>, UserValidator>();

            return services;
        }
    }
}