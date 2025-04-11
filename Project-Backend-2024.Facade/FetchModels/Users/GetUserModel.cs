using Project_Backend_2024.DTO.Enums;
using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.Facade.GetModels;

public class GetUserModel
{
    public string? Id { get; set; } 
    public string? UserName { get; set; } 
    public string? Email { get; set; }
    public string? ImageUrl { get; set; }
    public string? Bio { get; set; }
    public RegistrationType? RegistrationType { get; set; }
}