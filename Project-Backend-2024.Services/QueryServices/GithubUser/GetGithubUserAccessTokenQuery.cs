using MediatR;
using Project_Backend_2024.Facade.FetchModels;

namespace Project_Backend_2024.Services.QueryServices.GithubUser;

public class GetGithubUserAccessTokenQuery : IRequestHandler<GetGithubAccessToken,string>
{
    public async Task<string> Handle(GetGithubAccessToken request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Code))
            throw new Exception("Authorization code is missing.");
        
        using var httpClient = new HttpClient();
        
        var tokenRequest = new Dictionary<string, string>
        {
            { "client_id", "Iv23lidUetpHsRCSlAaY" },
            { "client_secret", "e55778f0631826dbf7151868cafe09570c904c75" },
            { "code", request.Code }
        };
        
        var response = await httpClient.PostAsync("https://github.com/login/oauth/access_token",
            new FormUrlEncodedContent(tokenRequest), cancellationToken);
        
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to exchange code for access token.");
        }
        
        // Extract access_token from response (GitHub returns it as a query string)
        var queryParams = System.Web.HttpUtility.ParseQueryString(responseBody);
        var accessToken = queryParams["access_token"] ?? throw new Exception();

        return accessToken;
    }
}