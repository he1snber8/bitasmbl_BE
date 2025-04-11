using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class UserSkills : IBaseEntity
{
    // Foreign Keys
    public required string UserId { get; set; }
    public int SkillId { get; set; }

    // Navigation Properties
    public User? User { get; set; } 
    public Skill? Skill { get; set; } 
}

