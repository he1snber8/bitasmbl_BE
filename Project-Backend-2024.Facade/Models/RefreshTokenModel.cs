namespace Project_Backend_2024.Facade.Models;

public class RefreshTokenModel : IEntityModel
{
    public int Id { get; }
    public string Token { get; set; } = null!;
    public string UserID { get; set; } = null!;
    public DateTime ExpirationDate { get; set; }
    public bool isActive { get; set; }
}
