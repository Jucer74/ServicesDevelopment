using AutoMapper;
using FluentValidation;
using Application.Dtos;
using Application.Mapping;
using Application.Services;
using Application.Validations;
using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Infrastructure.Repositories;

namespace TeamsApi.Extensions;

public static class ModulesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<ITeamMemberService, TeamMemberService>();

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
        services.AddScoped<IValidator<TeamMemberDto>, TeamMemberValidator>();

        return services;
    }
    public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
    {
        //repositories
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();

        return services;
    }

}
