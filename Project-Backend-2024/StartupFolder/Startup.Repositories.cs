using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Repositories;

namespace Project_Backend_2024.StartupFolder;

internal static partial class Startup
{
    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>()
            // .AddScoped<ISkillRepository, SkillRepository>()
            .AddScoped<IOrganizationManagerRepository, OrganizationManagerRepository>()
            .AddScoped<ITeamManagerRepository, TeamManagerRepository>()
            .AddScoped<IProjectRepository, ProjectRepository>()
            // .AddScoped<IProjectApplicationRepository, ProjectApplicationRepository>()
            // .AddScoped<IUserSkillsRepository, UserSkillsRepository>()
            // .AddScoped<IProjectRequirementRepository, ProjectRequirementRepository>()
            .AddScoped<ITransactionRepository, TransactionRepository>()
            .AddScoped<ITeamRepository, TeamRepository>()
            .AddScoped<IOrganizationRepository, OrganizationRepository>()
            .AddScoped<IRequirementRepository, RequirementRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>();
    }
}
