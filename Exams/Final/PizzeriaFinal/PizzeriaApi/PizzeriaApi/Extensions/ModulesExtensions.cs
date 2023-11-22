using AutoMapper;
using FluentValidation;
using PizzeriaApi.Dtos;
using PizzeriaApi.Mapping;
using PizzeriaApi.Services;
using PizzeriaApi.Validations;

namespace PizzeriaApi.Extensions;

public static class ModulesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPizzeriaService, PizzeriaService>();
        services.AddScoped<IPizzeriaCategoriaService, PizzeriaCategoriaService>();

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
        services.AddScoped<IValidator<PizzeriaDto>, PizzeriaValidator>();
        services.AddScoped<IValidator<PizzeriaCategoriaDto>, PizzeriaCategoriaValidator>();

        return services;
    }
}
