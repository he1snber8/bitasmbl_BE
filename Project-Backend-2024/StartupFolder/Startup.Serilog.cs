using Serilog;

namespace Project_Backend_2024.StartupFolder
{
    internal static partial class Startup
    {
       internal static void ConfigureSerilog(this WebApplicationBuilder webApplicationBuilder, IConfiguration configuration)
       {
           webApplicationBuilder.Host.UseSerilog((context,config) =>
           {
               config.ReadFrom.Configuration(configuration.GetSection("Serilog"));
           });
       }
    }
}
