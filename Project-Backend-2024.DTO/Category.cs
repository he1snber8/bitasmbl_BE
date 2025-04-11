using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class Category : IEntity
{
    public int Id { get; set; } 
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    // public ICollection<ProjectCategory> ProjectCategories { get; set; } = new List<ProjectCategory>();
}