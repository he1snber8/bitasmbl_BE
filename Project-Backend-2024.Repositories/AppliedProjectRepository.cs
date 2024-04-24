using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Repositories;

public class AppliedProjectRepository : RepositoryBase<AppliedProject>, IRepositoryBase<AppliedProject>
{
    public AppliedProjectRepository(DatabaseContext context) : base(context) { }
}