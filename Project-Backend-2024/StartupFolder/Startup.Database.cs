
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Repositories;

namespace Project_Backend_2024.StartupFolder;

internal static partial class Startup
{
    internal static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MySQLConnection");
        
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
}
