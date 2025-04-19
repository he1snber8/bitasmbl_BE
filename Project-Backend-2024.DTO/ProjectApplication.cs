using Project_Backend_2024.DTO.Enums;
using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class ProjectApplication : IEntity
{
    public int Id { get; set; }

    public int TeamId { get; set; }
    public int ProjectId { get; set; }
    public string? CoverLetter { get; set; }

    public DateTime DateApplied { get; set; } = DateTime.UtcNow;

    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    
    public Team? Team { get; set; }
    public Project Project { get; set; }
  
    // public int Id { get; init; } // Fixed to include a setter for EF
    // public string? PrincipalId { get; set; }
    // public ApplicationStatus? ApplicationStatus { get; set; }
    // public DateTime? DateApplied { get; set; }
    // public string? CoverLetter { get; set; }
    //
    // public required List<string> SelectedAndAppliedRequirements { get; set; }
    // public double QuizScore { get; set; }
    // // Foreign Keys
    // public int ProjectId { get; set; }
    // public required string ApplicantId { get; set; }
    //
    // // public bool IsTestEnabled { get; set; } = false;
    //
    // // Navigation Properties
    // public User? Applicant { get; set; }
    // public Project? Project { get; set; } 
}

// public class TeamApplication : IEntity
// {
//     public int Id { get; set; }
//
//     public int TeamId { get; set; }
//
//     public string? CoverLetter { get; set; }
//
//     public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
//
//     public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
//     
//     public Team? Team { get; set; }
//
//     public Project Project { get; set; }
//     public int ProjectId { get; set; }
// }
