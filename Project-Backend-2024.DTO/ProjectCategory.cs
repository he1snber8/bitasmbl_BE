using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class ProjectCategory : IBaseEntity
{
    public int ProjectId { get; set; }
    public int CategoryId { get; set; }

    // Navigation Properties
    public Project? Project { get; set; } 
    public Category? Category { get; set; } 
}