using Microsoft.AspNetCore.Http;
using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class Organization : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Industry { get; set; }

    public int OrganizationSize { get; set; }  // e.g., number of employees

    public bool IsVerified { get; set; } = false;

    public string? WebsiteUrl { get; set; }

    public string? LogoUrl { get; set; } // Optional

    public string? Country { get; set; } 
    
    public string ContactEmail { get; set; } = string.Empty;
    
    
}