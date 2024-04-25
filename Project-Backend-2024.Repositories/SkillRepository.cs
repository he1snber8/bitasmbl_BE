using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Repositories;

public class SkillRepository : RepositoryBase<Skill>, ISkillRepository
{
    public SkillRepository(DatabaseContext context) : base(context) { }
}
