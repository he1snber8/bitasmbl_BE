using MediatR;

namespace Project_Backend_2024.Facade.FetchModels;

public class GithubRepo
{
    public string Name { get; set; }  // Repo name
    public string Html_Url { get; set; }  // Repo URL
    public string Url { get; set; }  // Repo URL
    public string Description { get; set; }  // Repo description
    public string Language { get; set; }  // Main programming language
    public int Stargazers_Count { get; set; }  // Star count
    public int Forks_Count { get; set; }  // Fork count
    public bool Private { get; set; }  // Whether repo is private
    public string Visibility { get; set; }  // "public" or "private"

    public GithubUser Owner { get; set; }  // Repo owner details
    
    public ContributionHeatmap Contributions { get; set; }  // Contributions heatmap data (optional)
    public int Open_Issues_Count { get; set; }  // Count of open issues
    public string Created_At { get; set; }  // ISO format for repo creation date
    public string Updated_At { get; set; }  // ISO format for last updated date
}

public class ContributionHeatmap
{
    // Assuming the heatmap contains weekly data for contributions
    public List<ContributionWeek> Weeks { get; set; }
}

public class ContributionWeek
{
    public string Date { get; set; }  // Start date of the week
    public int Count { get; set; }  // Number of contributions in that week
}

public record GetGithubRepos(string AccessToken, string ReposUrl) : IRequest<GithubRepo[]>;