using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class RefreshToken : IEntity
{
    public int Id { get; }
    public string Token { get; set; } = null!;
    public DateTime ExpirationDate { get; set; }
    public bool? isActive { get; set; }

    public User? User { get; set; }
    public string UserID { get; set; } = null!;
}
