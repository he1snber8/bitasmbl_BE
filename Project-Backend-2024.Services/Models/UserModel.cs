namespace Project_Backend_2024.Services.Models;

internal class UserModel
{
    public int Id { get; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public byte[]? Picture { get; set; }
}
