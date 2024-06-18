namespace Project_Backend_2024.Facade.Models;

public class ProjectApplicationModel : IEntityModel
{   
    public int Id { get; }
    public string? CoverLetter { get; set; }
    public string? ApplicationStatus { get; set; }
    public int ProjectID { get; set; }
    public int UserID { get; set; }
}
