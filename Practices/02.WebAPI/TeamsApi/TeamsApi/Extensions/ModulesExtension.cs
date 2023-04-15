using AutoMapper;
using TeamsApi.Services;

namespace TeamsApi.Extensions;

public static class ModulesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<ITeamService, TeamService>();

        return services;
    }

    public static IServiceCollection AddMappinmg(this IServiceCollection services)
    {
        services.AddSingleton<IMapper, Mapper>(); ;

        return services;
    }


}
