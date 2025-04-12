using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Repositories;

namespace Project_Backend_2024.StartupFolder;

internal static partial class Startup
{
    private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var dbSection = configuration.GetSection("Database");
        
        var server = Environment.GetEnvironmentVariable("DB_SERVER") ?? dbSection["Server"];
        var database = Environment.GetEnvironmentVariable("DB_NAME") ?? dbSection["Database"];
        var userId = Environment.GetEnvironmentVariable("DB_USER") ?? dbSection["User"];
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? dbSection["Password"];
        
        var connectionString =
            $"server={server};database={database};User ID={userId};Password={password};TrustServerCertificate=true";


        services.AddDbContext<DatabaseContext>(options => { options.UseSqlServer(connectionString); });
    }
}