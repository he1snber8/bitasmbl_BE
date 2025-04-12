﻿using Microsoft.AspNetCore.Authorization;

namespace Project_Backend_2024;

internal static partial class Startup
{
    internal static void ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("UserOnly", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole( "User");
            });

            options.AddPolicy("AdminOnly", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Admin");
            });

            options.AddPolicy("AdminOrUser", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Admin", "User");
            });
        });
    }
}
