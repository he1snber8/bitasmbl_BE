namespace Project_Backend_2024.Services.Models;

internal class ProjectModel
{
    public int Id { get; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public string Status { get; set; } = null!;
}
