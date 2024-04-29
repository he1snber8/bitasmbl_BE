namespace Project_Backend_2024.Services.Models;

public class SkillModel : IEntityModel
{
    public int Id { get; }
    public string Name { get; set; } = null!;
}