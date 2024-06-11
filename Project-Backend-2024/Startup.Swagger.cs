using Microsoft.OpenApi.Models;

namespace Project_Backend_2024;

internal static partial class Startup
{
    internal static void AddSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseSwagger();
            app.UseSwaggerUI();
    }

    internal static void AddSwaggerBearer(this IServiceCollection services)
    {
        services.AddSwaggerGen(opts =>
        {
            opts.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            opts.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                 {
                    new OpenApiSecurityScheme
                     {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "BearerAuth"
                    }
                },
                new List<string>()
            }
         });
        });
    }
}

