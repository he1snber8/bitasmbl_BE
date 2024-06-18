using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;

namespace Project_Backend_2024.StartupFolder;

internal static partial class Startup
{
    internal static void AddMiddleWare(this WebApplication app)
    {
        app.UseSerilogRequestLogging()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseHttpsRedirection()
            .UseSwagger()
            .UseSwaggerUI();
        
            app.MapControllers();
            app.UseStatusCodePages(async context =>
            {
            var response = context.HttpContext.Response;

            switch (response.StatusCode)
            {
                case 404:
                    response.ContentType = "text/plain";
                    await response.WriteAsync("You are not authorized");
                    break;
                case 401:
                    response.ContentType = "text/plain";
                    await response.WriteAsync("NO NO NO!");
                    break;
            }
             });
    }
}
