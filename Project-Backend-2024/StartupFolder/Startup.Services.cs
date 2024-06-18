using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Authentication.AuthorizationServices;
using Project_Backend_2024.Services.CommandServices;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Services.Interfaces.Queries;
using Project_Backend_2024.Services.QueryServices;
using Project_Backend_2024.Services.Authentication.TokenGenerators;
using Project_Backend_2024.Services.Caching;
using Project_Backend_2024.Services.TokenValidators;

namespace Project_Backend_2024.StartupFolder;

internal static partial class Startup
{
    internal static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectCommandService, ProjectCommandService>()
                .AddScoped<IAppliedProjectCommandService, ProjectApplicationCommandService>()
                .AddScoped<ISkillCommandService, SkillCommandService>()
                .AddScoped<IUserSkillsCommandService, UserSkillsCommandService>()
                .AddScoped<IAuthorizableService, AuthorizableService>();

        services.AddScoped<IUserQueryService, UserQueryService>()
                .AddScoped<IProjectQueryService, ProjectQueryService>();


        services.AddScoped<AccessTokenGenerator>()
            .AddScoped<RefreshTokenGenerator>()
            .AddScoped<TokenValidator>()
            .AddScoped<CachingService>();

        // services.AddScoped<RoleInitializer>();
    }
}
