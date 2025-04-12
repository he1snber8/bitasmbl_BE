using Microsoft.AspNetCore.Authentication.Cookies;
using Project_Backend_2024.Services.Configurations;

namespace Project_Backend_2024.StartupFolder;

internal static partial class Startup
{
    private static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var cacheAuthConfiguration = new CacheConfiguration();
        configuration.Bind("Cache", cacheAuthConfiguration);
        services.AddSingleton(cacheAuthConfiguration);

        var cookieConfiguration = new CookieConfiguration();
        configuration.Bind("Cookie", cookieConfiguration);
        services.AddSingleton(cookieConfiguration);

        var s3ClientConfiguration = new S3ClientConfiguration();
        configuration.Bind("S3Settings", s3ClientConfiguration);
        services.AddSingleton(s3ClientConfiguration);

        services.AddEndpointsApiExplorer();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddGitHub(options =>
            {
                options.ClientSecret = "e55778f0631826dbf7151868cafe09570c904c75";
                options.ClientId = "Iv23lidUetpHsRCSlAaY";
            })
            .AddCookie(options =>
            {
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromSeconds(cookieConfiguration.ExpirationSeconds);
                options.Cookie.Name = "BitasmblCookie";
                options.Cookie.HttpOnly = true; // Prevents JavaScript from accessing the cookie
                options.Cookie.SameSite = SameSiteMode.None; // Allow cross-site requests
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure HTTPS cookies work
            });

        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddMemoryCache();
    }
}