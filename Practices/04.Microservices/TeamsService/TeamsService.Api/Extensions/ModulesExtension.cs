using AutoMapper;
using FluentValidation;
using TeamsService.Api.Dtos;
using TeamsService.Api.Mapping;
using TeamsService.Api.Validators;
using TeamsService.Application.Interfaces;
using TeamsService.Application.Services;
using TeamsService.Domain.Interfaces.Repositories;
using TeamsService.Infrastructure.Repositories;

namespace TeamsService.Api.Extensions;

public static class ModulesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITeamService, TeamService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();

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
        services.AddScoped<IValidator<TeamDto>, TeamValidator>();

        return services;
    }
}