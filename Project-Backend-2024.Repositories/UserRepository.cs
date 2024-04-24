using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Repositories;

public class UserRepository : RepositoryBase<User>, IRepositoryBase<User>
{
    public UserRepository(DatabaseContext context) : base(context) { }
}
