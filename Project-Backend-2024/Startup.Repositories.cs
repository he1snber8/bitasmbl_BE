using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024;

internal static partial class Startup
{
    internal static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IAppliedProjectRepository, AppliedProjectRepository>();
        services.AddScoped<IUserSkillsRepository, UserSkillsRepository>();
    }
}
