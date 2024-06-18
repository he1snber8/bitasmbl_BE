using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class ProjectApplication : IEntity
{
    public int Id { get; }
    public string? ApplicationStatus { get; set; }
    public DateTime? DateApplied{ get; set; }
    public string? CoverLetter { get; set; }

    public User? User { get; set; }
    public string UserID { get; set; } = null!;

    public Project? Project { get; set; }
    public int ProjectID { get; set; }

}