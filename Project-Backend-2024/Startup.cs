using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.Services;

namespace Project_Backend_2024;

internal static partial class Startup
{
    internal static IServiceCollection AddConfigurations(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Services.ConfigureAuthentication(configuration);
        builder.Services.ConfigureAuthorization(configuration);
        builder.Services.ConfigureDatabase(configuration);
        builder.Services.ConfigureIdentity();
        builder.Services.ConfigureRepositories();
        builder.Services.ConfigureServices();
        builder.Services.AddSwaggerBearer();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.ConfigureSerilog(configuration);

        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(Mappers).Assembly);

        return builder.Services;
    }

    internal static void AddAppMiddleware(this WebApplication app)
    {
        app.AddSwagger();
        app.AddMiddleWare();
    }

}
        