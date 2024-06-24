using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Repositories;

public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
{
    public ProjectRepository(DatabaseContext context) : base(context) { }
}