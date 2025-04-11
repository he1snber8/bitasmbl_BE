using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Facade.AdminModels;

public class ProjectModel : IEntityModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public DateTime? DateCreated { get; set; }
    public string PrincipalId { get; set; } = null!;
    public ICollection<ProjectApplicationModel>? Applications { get; set; } 
}