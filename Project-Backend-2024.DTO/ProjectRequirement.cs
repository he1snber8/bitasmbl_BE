using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class  ProjectRequirement : IBaseEntity
{
    public int ProjectId { get; set; }
    public int RequirementId { get; set; }

    public bool IsTestEnabled { get; set; }

    public byte MaxApplicationLimit { get; set; }
    
    public byte CurrentApplications { get; set; }
    
    
    // Navigation Properties
    
    // public RequirementTest RequirementTest { get; set; }
    public Project? Project { get; set; } 
    public Requirement? Requirement { get; set; } 
}