using Project_Backend_2024.Facade;

namespace Project_Backend_2024.DTO;

public class Skill : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
