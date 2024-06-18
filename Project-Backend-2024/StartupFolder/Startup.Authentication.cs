using Microsoft.AspNetCore.Authentication.Cookies;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.StartupFolder;
internal static partial class Startup
{
    internal static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var cacheAuthConfiguration = new CacheConfiguration();
        configuration.Bind("Cache", cacheAuthConfiguration);
        services.AddSingleton(cacheAuthConfiguration);
        
        var cookieConfiguration = new CookieConfiguration();
        configuration.Bind("Cookie", cookieConfiguration);
        services.AddSingleton(cookieConfiguration);

        services.AddEndpointsApiExplorer();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
              options.SlidingExpiration = true; 
              options.ExpireTimeSpan = TimeSpan.FromSeconds(cookieConfiguration.ExpirationSeconds);
            });
        
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddMemoryCache();
    }
}


