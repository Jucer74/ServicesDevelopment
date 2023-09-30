using AutoMapper;
using FluentValidation;
using MembersService.Api.Mapping;
using MembersService.Application.Interfaces;
using MembersService.Domain.Dtos;
using MembersService.Domain.Interfaces;
using MembersService.Domain.Validators;
using MembersService.Infrastructure.Repositories;
using MembersService.Application.Services;

namespace MembersService.Api.Extensions;

public static class ModulesExtension
{
    public static IServiceCollection AddCoreModules(this IServiceCollection services)
    {
        services.AddScoped<IMemberService, MemberService>();
        return services;
    }

    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
        return services;
    }

    public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IMemberRepository, MemberRepository>();


        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<MemberDto>, MemberValidator>();

        return services;
    }
}
