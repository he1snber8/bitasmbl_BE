using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services;
using Project_Backend_2024.Services.CommandServices;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Services.TokenGenerators;
using Project_Backend_2024.Services.TokenGenerators.Interfaces;

namespace Project_Backend_2024;

internal static class Startup
{

    internal static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Bind JWT configuration
        var authConfiguration = new AuthConfiguration();
        configuration.Bind("JWT", authConfiguration);
        services.AddSingleton(authConfiguration);

        // Add database context
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("MSSQLConnection"));
        });

        // Register repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IAppliedProjectRepository, AppliedProjectRepository>();
        services.AddScoped<IUserSkillsRepository, UserSkillsRepository>();

        // Register command services
        services.AddScoped<IUserCommandService, UserCommandService>();
        services.AddScoped<IProjectCommandService, ProjectCommandService>();
        services.AddScoped<IAppliedProjectCommandService, AppliedProjectCommandService>();
        services.AddScoped<ISkillCommandService, SkillCommandService>();
        services.AddScoped<IUserSkillsCommandService, UserSkillsCommandService>();

        // Register query services

        // Register other services
        services.AddSingleton<IUserAccessToken, AccessTokenGenerator>();
        services.AddControllers();
        services.AddAutoMapper(typeof(Mappers).Assembly);

        return services;
    }

    internal static void UseSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseSwagger();
            app.UseSwaggerUI();
    }
}
