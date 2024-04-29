namespace Project_Backend_2024.Services.Models;

public class AppliedProjectModel : IEntityModel
{   
    public int Id { get; }
    public string? CoverLetter { get; set; }
    public string? ApplicationStatus { get; set; }
    public int ProjectID { get; set; }
    public int UserID { get; set; }
}
