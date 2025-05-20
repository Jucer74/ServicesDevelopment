using AutoMapper;
using FluentValidation;
using MoneyBankService.Application.Dto;
using MoneyBankService.Api.Mappers;
using MoneyBankService.Api.Validators;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Application.Services;
using MoneyBankService.Domain.Interfaces.Repositories;
using MoneyBankService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MoneyBankService.Api.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            return services;
        }

        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            services.AddSingleton(mapperConfig.CreateMapper());
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AccountValidator>();
            return services;
        }

        public static IServiceCollection AddApplicationModules(this IServiceCollection services)
        {
            return services
                .AddServices()
                .AddRepositories()
                .AddMapping()
                .AddValidators(); 
        }
    }
}