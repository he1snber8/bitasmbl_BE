
using Microsoft.AspNetCore.Authorization;

namespace Project_Backend_2024.Services.Authentication.Authorization;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(params Permission [] permission) : base(policy: permission.ToString())
    {
        
    }
}

public enum Permission
{
    ReadAccess = 1,
    WriteAccess = 2
}
