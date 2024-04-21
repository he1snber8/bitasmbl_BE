using Project_Backend_2024.Facade;

namespace Project_Backend_2024.DTO;

public class Project : IEntity, IDeletable
{
    public int Id { get; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime? DateCreated { get; set; }
    
    public User? User { get; set; }
    public int PrincipalID { get; set; }

}
