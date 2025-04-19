using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class UserSocialLink : IEntity
{
    public int Id { get; }
    public required string UserId { get; set; }
    public required string SocialUrl { get; set; }
    public required TeamManager User { get; set; }
    
}