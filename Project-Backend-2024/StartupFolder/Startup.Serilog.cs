using Serilog;

namespace Project_Backend_2024.StartupFolder
{
    internal static partial class Startup
    {
       internal static void ConfigureSerilog(this WebApplicationBuilder webApplicationBuilder)
       {
           webApplicationBuilder.Host.UseSerilog((context, loggerConfiguration) =>
           {
               loggerConfiguration.ReadFrom.Configuration(context.Configuration);
           });

       }
    }
}
