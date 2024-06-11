using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services;

namespace Project_Backend_2024;

internal static partial class Startup
{
    public static void ConfigureApp(this IServiceCollection services, IConfiguration configuration)
    {     
        services.AddAutoMapper(typeof(Mappers).Assembly);
        ConfigureDatabase(services, configuration);
        ConfigureIdentity(services);
        ConfigureRepositories(services);
        ConfigureAuthentication(services, configuration);
        ConfigureAuthorization(services);
        AddSwaggerBearer(services);
        ConfigureServices(services);
    }
}
