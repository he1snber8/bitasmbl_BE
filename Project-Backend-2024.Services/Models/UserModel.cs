namespace Project_Backend_2024.Services.Models;

public class UserModel : IEntityModel, IBasicModel
{
    public int Id { get; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public byte[]? Picture { get; set; }
}
