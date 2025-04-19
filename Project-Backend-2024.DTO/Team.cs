using Microsoft.AspNetCore.Http;
using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class Team : IEntity
{
    public int Id { get; set; }
    
    public required string Name { get; set; }

    public int TeamManagerId { get; set; }
    public string? ContactName { get; set; }

    public required string ContactEmail { get; set; } 

    public string? Location { get; set; }

    public int YearsInOperation { get; set; }

    public string? CompanyProfileUrl { get; set; }

    public string? LogoUrl{ get; set; }

    public TeamManager TeamManager { get; set; }
    public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
    
    public ICollection<ProjectApplication> ProjectApplications { get; set; } = new List<ProjectApplication>();
}