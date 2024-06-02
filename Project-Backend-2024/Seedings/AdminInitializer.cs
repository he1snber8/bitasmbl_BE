using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.Services.Authentication.Authorization;

namespace Project_Backend_2024.Seedings;

public class AdminInitializer
{
    private readonly IAuthorizableService _authorizableService;

    public AdminInitializer(IAuthorizableService authorizableService)
    {
       _authorizableService = authorizableService;
    }
}