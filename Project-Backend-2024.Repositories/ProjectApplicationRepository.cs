using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Repositories;

public class ProjectApplicationRepository : RepositoryBase<ProjectApplication>, IProjectApplicationRepository
{
    public ProjectApplicationRepository(DatabaseContext context) : base(context) { }
}