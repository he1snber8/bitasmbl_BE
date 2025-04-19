
using Project_Backend_2024.DTO.Enums;
using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

    public class Project : IEntity
    {
        public int Id { get; set; } // Fixed to include a setter for EF
        public bool IsDeleted { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required ProjectStatus Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public required string PrincipalId { get; set; }
        public required TeamManager User { get; set; }
        public ICollection<ProjectApplication> ProjectApplications { get; set; } = new List<ProjectApplication>();
        public ICollection<ProjectRequirement> ProjectRequirements { get; set; } = new List<ProjectRequirement>();
        public ICollection<ProjectCategory> ProjectCategories { get; set; } = new List<ProjectCategory>();
        
        // public ICollection<ProjectLink>? ProjectLinks { get; set; } = new List<ProjectLink>();
        // public ICollection<ProjectImage>? ProjectImages { get; set; } = new List<ProjectImage>();
        // public ICollection<ProjectTask>? ProjectTasks { get; set; } = new List<ProjectTask>();
        
    }
