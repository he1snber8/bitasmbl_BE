using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class TeamMember : IEntity
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Email { get; set; }

    public string? Role { get; set; } // Optional: Dev, Designer, PM, etc.
    
    public string? ResumeUrl { get; set; } 

    public string? LinkedInUrl { get; set; } // Optional portfolio/social

    public string? Bio { get; set; } // Optional short intro

    public int TeamId { get; set; }
    public Team Team { get; set; }
}

public class TeamMemberModel 
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Email { get; set; }

    public string? Role { get; set; } // Optional: Dev, Designer, PM, etc.
    
    public string? ResumeUrl { get; set; } 

    public string? LinkedInUrl { get; set; } // Optional portfolio/social

    public string? Bio { get; set; } // Optional short intro
}