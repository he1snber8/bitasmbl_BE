using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.Exceptions;

namespace Project_Backend_2024;

public sealed class ProjectsHub() : Hub
{
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var principalId = httpContext?.Request.Query["principalId"];

        if (!string.IsNullOrEmpty(principalId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"principal_{principalId}");
            Console.WriteLine($"✅ Principal {principalId} added to group 'principal_{principalId}'");
        }
    }

    public async Task SendMessage(string content)
    {
        // Groups.AddToGroupAsync()
        await Clients.All.SendAsync("ReceiveMessage", content);
    }
    
    public async Task NotifyProjectCreated(string projectId)
    {
        await Clients.All.SendAsync("ProjectCreated", projectId);
    }
    
    // public async Task NotifyProjectApplied(ApplyToProjectModel applyToProjectModel)
    // {
    //    
    //    var userId = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value
    //         ?? throw new UserNotFoundException();
    //     
    //     await Clients.User(userId).SendAsync("ProjectApplied", applyToProjectModel);
    // }
}