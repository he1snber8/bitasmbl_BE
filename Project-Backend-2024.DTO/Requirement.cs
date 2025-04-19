using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class Requirement : IEntity
{
    public int Id { get; set; } // Fixed to include a setter for EF
    public required string Name { get; set; }

    public User User { get; set; }
    
}