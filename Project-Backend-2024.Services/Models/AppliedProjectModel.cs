namespace Project_Backend_2024.Services.Models;

internal class AppliedProjectModel
{
    public string? CoverLetter { get; set; }
    public string ApplicationStatus { get; set; } = null!;
    public int ProjectID { get; set; }
    public int UserID { get; set; }
}
