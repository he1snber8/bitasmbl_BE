namespace Project_Backend_2024.Facade.FetchModels;

public class GoogleUser
{
    public required string Id { get; set; } // Default provider as GitHub
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Picture { get; set; }
    public string Provider { get; set; } = "google"; // Default provider as GitHub
    public string? Family_Name { get; set; }
    public string? Given_Name { get; set; }
    public bool? Verified_Email { get; set; }
}