namespace Project_Backend_2024.Services.Models;

public class ProjectModel : IEntityModel, IBasicModel
{
    public int Id { get; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public string Status { get; set; } = null!;
}
