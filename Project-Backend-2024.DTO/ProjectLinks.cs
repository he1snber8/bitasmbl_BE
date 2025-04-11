using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class  ProjectLink : IBaseEntity
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string UrlName { get; set; } = string.Empty; // Initialize to avoid nulls.
    public string UrlValue { get; set; } = string.Empty;
    
    public Project? Project { get; set; } 
}