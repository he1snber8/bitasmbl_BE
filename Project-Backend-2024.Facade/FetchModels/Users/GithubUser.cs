using MediatR;

namespace Project_Backend_2024.Facade.FetchModels;

public class GithubUser
{
    public string Provider { get; set; } = "github"; // Default provider as GitHub
    public string? Login { get; set; }
    public string? Avatar_Url { get; set; }
    public string? Email { get; set; } 
    public int Followers { get; set; }
    public int Following { get; set; }
    public int Public_Repos { get; set; } // Maps to `public_repos`
    public int Public_Gists { get; set; } // Maps to `public_gists`
    public string? Created_At { get; set; } // Maps to `created_at`
    public string? Repos_Url { get; set; }
    public string? Url { get; set; } // Maps to `repos_url`
    public string? Updated_At { get; set; } // Maps to `updated_at`
    public string? Location { get; set; }
    public string? Twitter_Username { get; set; }
    public bool? Hireable { get; set; }
}

//Github Auth
public record GetGithubAccessToken(string Code) : IRequest<string>;
