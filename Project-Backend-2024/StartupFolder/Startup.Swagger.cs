using Microsoft.OpenApi.Models;

namespace Project_Backend_2024.StartupFolder;

internal static partial class Startup
{
    private static void AddSwaggerBearer(this IServiceCollection services)
    {
        services.AddSwaggerGen(opts =>
        {
            opts.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });
    }
}

