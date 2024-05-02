namespace Project_Backend_2024.Facade.Models;

public class ProjectModel : IEntityModel
{
    public int Id { get; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public int PrincipalID { get; set; }
}
