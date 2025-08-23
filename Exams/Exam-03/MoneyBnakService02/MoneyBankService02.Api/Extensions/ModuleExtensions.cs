using AutoMapper;
using FluentValidation;
using MoneyBankService02.Application.Interfaces;
using MoneyBankService02.Application.Services;
using MoneyBankService02.Domain.Interfaces.Repositories;
using MoneyBankService02.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MoneyBankService02.Api.Mappers;
using MoneyBankService02.Api.Validators;

namespace MoneyBankService02.Api.Extensions;

public static class ModuleExtensions
{
    public static IServiceCollection AddApplicationModules(this IServiceCollection services)
    {
        return services
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IAccountRepository, AccountRepository>()
            .AddSingleton(new MapperConfiguration(mc => mc.AddProfile(new MappingProfile())).CreateMapper())
            .AddValidatorsFromAssemblyContaining<AccountValidator>();
    }
}
