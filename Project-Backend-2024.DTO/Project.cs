
namespace Project_Backend_2024.DTO;

public class Project : IEntity, IDeletable
{
    public int Id { get; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } 
    public string? Status { get; set; } 
    public DateTime? DateCreated { get; set; }
    
    public User? User { get; set; }
    public int PrincipalID { get; set; }

}
