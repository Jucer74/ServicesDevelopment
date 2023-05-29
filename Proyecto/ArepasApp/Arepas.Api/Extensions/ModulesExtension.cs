using Arepas.Api.Dtos;
using Arepas.Api.Validators;
using Arepas.Application.Interfaces;
using Arepas.Application.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Arepas.Api.Extensions;

public static class ModulesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CustomerDto>, CustomerValidator>();

        return services;
    }

    public static IServiceCollection AddMappinmg(this IServiceCollection services)
    {
        services.AddScoped<IMapper, Mapper>(); ;

        return services;
    }
}