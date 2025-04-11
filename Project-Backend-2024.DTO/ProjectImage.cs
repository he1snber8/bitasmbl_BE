using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class ProjectImage : IBaseEntity
{
    public required string ImageUrl { get; set; }
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
}