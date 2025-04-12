namespace Project_Backend_2024.StartupFolder;

internal static partial class Startup
{
    private static void ConfigureCors(this IServiceCollection services)
    {
        var allowedOrigin1 = Environment.GetEnvironmentVariable("ALLOWED_ORIGIN_1") ?? "";
        var allowedOrigin2 = Environment.GetEnvironmentVariable("ALLOWED_ORIGIN_2") ?? "";

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins(allowedOrigin1, allowedOrigin2)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });
    }
}
