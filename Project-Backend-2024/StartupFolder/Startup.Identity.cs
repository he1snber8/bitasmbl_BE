using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Repositories;

namespace Project_Backend_2024.StartupFolder;

internal static partial class Startup
{
    private static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 6,
                    RequireLowercase = true,
                    RequireUppercase = true,
                    RequiredUniqueChars = 1
                };
            })
               .AddEntityFrameworkStores<DatabaseContext>()
               .AddDefaultTokenProviders();
    }
}
