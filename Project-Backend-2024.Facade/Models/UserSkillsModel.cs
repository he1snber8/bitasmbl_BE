namespace Project_Backend_2024.Facade.Models;

public class UserSkillsModel : IEntityModel
{
    public int Id { get; }
    public int UserID { get; set; }
    public int SkillID { get; set; }
}
