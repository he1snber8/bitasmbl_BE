namespace Project_Backend_2024.DTO.Github;

public class GithubRepoBranches
{
    public string Name { get; set; }
    
    public bool Protected { get; set; }
    
    public GithubCommit Commit { get; set; }
}