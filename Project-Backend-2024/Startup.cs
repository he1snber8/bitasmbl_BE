using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services;
using Project_Backend_2024.Services.CommandServices;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Services.Interfaces.Queries;
using Project_Backend_2024.Services.QueryServices;
using Project_Backend_2024.Services.TokenGenerators;
using Project_Backend_2024.Services.TokenValidators;
using Serilog;
using System.Text;

namespace Project_Backend_2024;

internal static partial class Startup
{
    internal static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureAuthentication(configuration);
        services.ConfigureAuthorization(configuration);
        services.ConfigureDatabase(configuration);
        services.ConfigureIdentity();
        services.ConfigureRepositories();
        services.ConfigureServices();
        services.AddSwaggerBearer();


        services.AddControllers();
        services.AddAutoMapper(typeof(Mappers).Assembly);

        return services;
    }

    internal static void AddAppMiddleware(this WebApplication app)
    {
        app.AddSwagger();
        app.AddMiddleWare();
    }

    internal static void IncludeAppSettingsConfiguration(this WebApplicationBuilder webApplicationBuilder, IConfiguration configuration)
    {
        webApplicationBuilder.ConfigureSerilog(configuration);
    }
}
        