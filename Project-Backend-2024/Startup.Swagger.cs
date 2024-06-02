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
           opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { Scheme = "Bearer", In = ParameterLocation.Header, Type = SecuritySchemeType.Http, BearerFormat = "JWT" });

           opts.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
               { new OpenApiSecurityScheme { Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme } } , new List<string>()}
            });
       });
   }
}
