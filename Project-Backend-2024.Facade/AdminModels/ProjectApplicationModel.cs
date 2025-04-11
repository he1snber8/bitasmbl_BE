using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Facade.AdminModels;

public class ProjectApplicationModel : IEntityModel
{   
    public int Id { get; set; }

    public string PrincipalId { get; set; } = null!;
    
    public string? CoverLetter { get; set; }
    
    public string? ApplicationStatus { get; set; }
    
    public int ProjectId { get; set; }

    public string ApplicantId { get; set; } = null!;
    
    public string Applicant { get; set; } = null!;
}
