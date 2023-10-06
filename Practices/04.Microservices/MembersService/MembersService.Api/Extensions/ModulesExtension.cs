using AutoMapper;
using FluentValidation;
using MembersService.Api.Dtos;
using MembersService.Api.Mapping;
using MembersService.Api.Validators;
using MembersService.Application.Interfaces;
using MembersService.Application.Services;
using MembersService.Domain.Interfaces.Repositories;
using MembersService.Infrastructure.Repositories;

namespace MembersService.Api.Extensions;

public static class ModulesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMemberService, MemberService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
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
        services.AddScoped<IValidator<MemberDto>, MemberValidator>();

        return services;
    }
}