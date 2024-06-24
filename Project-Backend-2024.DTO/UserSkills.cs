using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class UserSkills : IEntity
{
    public int Id { get; }

    public User? User { get; set; }
    public string UserId { get; set; }

    public Skill? Skill { get; set; }
    public int SkillId { get; set; }
}