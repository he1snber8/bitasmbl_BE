using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Repositories;

namespace Project_Backend_2024.StartupFolder;

internal static partial class Startup
{
    internal static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<DatabaseContext>()
               .AddDefaultTokenProviders();
    }
}
