using Serilog;

namespace Project_Backend_2024;

internal static partial class Startup
{
    internal static void AddMiddleWare(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.AddSwagger();
        app.MapControllers();
    }

}
