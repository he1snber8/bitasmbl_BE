using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;

namespace Project_Backend_2024;

internal static partial class Startup
{
    internal static void AddMiddleWare(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        app.UseStatusCodePages(async context =>
        {
            var response = context.HttpContext.Response;

            switch (response.StatusCode)
            {
                case 404:
                    response.ContentType = "text/plain";
                    await response.WriteAsync("Hell Nah, you not authorized negro");
                    break;
                case 401:
                    response.ContentType = "text/plain";
                    await response.WriteAsync("NO NO NO!");
                    break;
            }
        });
    }
}
