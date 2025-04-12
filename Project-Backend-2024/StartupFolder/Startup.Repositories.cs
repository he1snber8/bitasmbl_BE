using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Repositories;

namespace Project_Backend_2024.StartupFolder;

internal static partial class Startup
{
    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<ISkillRepository, SkillRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IProjectRepository, ProjectRepository>()
            // .AddScoped<IProjectApplicationRepository, ProjectApplicationRepository>()
            .AddScoped<IUserSkillsRepository, UserSkillsRepository>()
            // .AddScoped<IProjectRequirementRepository, ProjectRequirementRepository>()
            .AddScoped<ITransactionRepository, TransactionRepository>()
            .AddScoped<IRequirementRepository, RequirementRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>();
    }
}
