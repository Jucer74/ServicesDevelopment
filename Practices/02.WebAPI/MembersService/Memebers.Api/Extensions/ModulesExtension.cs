﻿using AutoMapper;
using FluentValidation;
using Members.Api.Mapping;
using Members.Application.Interfaces;
using Members.Application.Services;
using Members.Domain.Dtos;
using Members.Domain.Interfaces;
using Members.Domain.Validators;
using Members.Infrastructure.Repositories;

namespace Members.Api.Extensions;

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