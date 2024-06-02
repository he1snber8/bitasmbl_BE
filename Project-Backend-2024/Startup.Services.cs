using Project_Backend_2024.Services.CommandServices;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Services.Interfaces.Queries;
using Project_Backend_2024.Services.QueryServices;

namespace Project_Backend_2024;

internal static partial class Startup
{
    internal static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectCommandService, ProjectCommandService>();
        services.AddScoped<IAppliedProjectCommandService, AppliedProjectCommandService>();
        services.AddScoped<ISkillCommandService, SkillCommandService>();
        services.AddScoped<IUserSkillsCommandService, UserSkillsCommandService>();

        services.AddScoped<IUserQueryService, UserQueryService>();
    }
}
