namespace Project_Backend_2024.Services.Models;

public class UserSkillsModel : IEntityModel
{
    public int Id { get; }
    public int UserID { get; set; }
    public int SkillID { get; set; }
}
