using Microsoft.EntityFrameworkCore;

namespace Project_Backend_2024.Repositories
{
    public class DatabaseConnector : DbContext
    {
        public DatabaseConnector(DbContextOptions<DatabaseConnector> opts) : base(opts) { }
    }
}