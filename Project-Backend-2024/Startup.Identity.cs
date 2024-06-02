using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024;

internal static partial class Startup
{
    internal static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<DatabaseContext>()
               .AddDefaultTokenProviders();
    }
}
