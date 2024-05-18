using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services;
using Project_Backend_2024.Services.CommandServices;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Services.Interfaces.Queries;
using Project_Backend_2024.Services.QueryServices;
using Project_Backend_2024.Services.TokenGenerators;
using Project_Backend_2024.Services.TokenValidators;
using Serilog;
using System.Text;

namespace Project_Backend_2024;

internal static class Startup
{

    internal static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Bind JWT configuration
        var authConfiguration = new AuthConfiguration();
        configuration.Bind("JWT", authConfiguration);
        services.AddSingleton(authConfiguration);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opts =>
        {
            opts.TokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.Key)),
                ValidIssuer = authConfiguration.Issuer,
                ValidAudience = authConfiguration.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero

            };
        });


        // Add database context
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("MSSQLConnection"));
        });

        // Register repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IAppliedProjectRepository, AppliedProjectRepository>();
        services.AddScoped<IUserSkillsRepository, UserSkillsRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        // Register command services
        services.AddScoped<IUserCommandService, UserCommandService>();
        services.AddScoped<IProjectCommandService, ProjectCommandService>();
        services.AddScoped<IAppliedProjectCommandService, AppliedProjectCommandService>();
        services.AddScoped<ISkillCommandService, SkillCommandService>();
        services.AddScoped<IUserSkillsCommandService, UserSkillsCommandService>();
        services.AddScoped<IRefreshTokenCommandService, RefreshTokenCommandService>();

        // Register query services
        services.AddScoped<IUserQueryService, UserQueryService>();
        services.AddScoped<IRefreshTokenQueryService, RefreshTokenQueryService>();

        // Register other services
        services.AddSingleton<AccessTokenGenerator>();
        services.AddSingleton<RefreshTokenGenerator>();
        services.AddSingleton<RefreshTokenValidator>();
        services.AddControllers();
        services.AddAutoMapper(typeof(Mappers).Assembly);

        return services;
    }

    internal static void ConfigureSerilog(this WebApplicationBuilder webApplicationBuilder, IConfiguration configuration)
    {
        webApplicationBuilder.Host.UseSerilog((context,config) =>
        {
            config.ReadFrom.Configuration(configuration.GetSection("Serilog"));
        });
    }

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
