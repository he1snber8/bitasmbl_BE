using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Project_Backend_2024.Facade.Models;
using System.Text;

namespace Project_Backend_2024;

internal static partial class Startup
{
    internal static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var userAuthConfiguration = new AuthConfiguration();
        configuration.Bind("JWT", userAuthConfiguration);
        services.AddSingleton(userAuthConfiguration);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opts =>
        {
            opts.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(userAuthConfiguration.Key)),
                ValidIssuer = userAuthConfiguration.Issuer,
                ValidAudience = userAuthConfiguration.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };

            opts.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Request.Cookies.TryGetValue("accessToken", out var accessToken);
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                }
            };
        });
    }
}
