using Microsoft.EntityFrameworkCore;

namespace Project_Backend_2024;

internal static partial class Startup
{
    internal static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("MSSQLConnection"));
        });
    }
}
