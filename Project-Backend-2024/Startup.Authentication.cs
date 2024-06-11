using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Authentication.Authorization;

namespace Project_Backend_2024;

internal static partial class Startup
{
    internal static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var userAuthConfiguration = new AuthConfiguration();
        configuration.Bind("JWT", userAuthConfiguration);
        services.AddSingleton(userAuthConfiguration);

        services.AddEndpointsApiExplorer();

        services.AddAuthentication("MyAuthScheme")
            .AddCookie("MyAuthScheme", options =>
            {
              options.LoginPath = "/Account/Login"; 
              options.LogoutPath = "/Account/Logout";
              //options.Cookie.HttpOnly = true;
              //options.Cookie.SameSite = SameSiteMode.None;
              //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

        services.AddHttpContextAccessor();

    }
}


