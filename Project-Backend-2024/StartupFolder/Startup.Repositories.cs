using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Repositories;

namespace Project_Backend_2024.StartupFolder;

internal static partial class Startup
{
    internal static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<ISkillRepository, SkillRepository>()
                .AddScoped<IProjectRepository, ProjectRepository>()
                .AddScoped<IAppliedProjectRepository, AppliedProjectRepository>()
                .AddScoped<IUserSkillsRepository, UserSkillsRepository>();
    }
}
