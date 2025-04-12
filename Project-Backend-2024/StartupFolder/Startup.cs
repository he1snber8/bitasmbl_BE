using Project_Backend_2024.Services;
using Project_Backend_2024.Services.CommandServices.Skills;

namespace Project_Backend_2024.StartupFolder;

internal static partial class Startup
{
    public static void ConfigureApp(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDatabase(configuration);
        services.ConfigureIdentity();
        services.ConfigureRepositories();
        services.ConfigureAuthentication(configuration);
        services.ConfigureAuthorization();
        services.AddSwaggerBearer();
        services.ConfigureServices();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(typeof(AddSkillCommand).Assembly);
        });
        
        services.AddAutoMapper(typeof(Mappers).Assembly);
        
    }
}
