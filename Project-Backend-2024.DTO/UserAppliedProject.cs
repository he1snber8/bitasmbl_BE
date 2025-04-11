using Project_Backend_2024.DTO.Enums;

namespace Project_Backend_2024.DTO;

public class UserAppliedProject
{
    public string UserId { get; set; }
    public int ProjectId { get; set; }
    
    public ApplicationStatus? ApplicationStatus { get; set; }
    public DateTime? DateApplied { get; set; }
    public string? CoverLetter { get; set; }
    
    public double QuizScore { get; set; }
    public required Project Project { get; set; }
    public required User User { get; set; }
}