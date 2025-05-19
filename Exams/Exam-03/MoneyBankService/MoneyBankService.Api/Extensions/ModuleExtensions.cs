using AutoMapper;
using FluentValidation;

using MoneyBankService.Api.Mappers;
using MoneyBankService.Application.Common.Interfaces;
using MoneyBankService.Application.DTO;
using MoneyBankService.Application.Services;
using MoneyBankService.Application.Services.Interfaces;
using MoneyBankService.Application.Services.Implementation;
using MoneyBankService.Application.Validations;
using MoneyBankService.Infrastructure.Repositories;

namespace MoneyBankService.Api.Extensions;

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
        services.AddScoped<IValidator<AccountCreateDto>, AccountValidator>();
        services.AddScoped<IValidator<TransactionCreateDto>, TransactionValidator>();


        return services;
    }
}
