using Amazon;
using Amazon.S3;
using Microsoft.Extensions.Options;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Authentication.CookieGenerators;
using Project_Backend_2024.Services.Caching;
using Project_Backend_2024.Services.CommandServices;
using Project_Backend_2024.Services.CommandServices.AWS;
using Project_Backend_2024.Services.CommandServices.Users;
using Project_Backend_2024.Services.Configurations;


namespace Project_Backend_2024.StartupFolder;

internal static partial class Startup
{
    internal static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserAuthentication, UserAuthorizationService>()
            .AddScoped<IUserRegistration, UserRegistrationCommand>()
            .AddScoped<IUserSignOut, UserSignOutCommand>()
            .AddScoped<ProjectsHub>();
        
        

        services.AddScoped<CachingService>()
            .AddScoped<CookieGenerator>()
            .AddScoped<S3BucketService>();
    }
}