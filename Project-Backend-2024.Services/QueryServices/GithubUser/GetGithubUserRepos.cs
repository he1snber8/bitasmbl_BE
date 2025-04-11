using System.Net.Http.Headers;
using System.Runtime.Serialization;
using MediatR;
using Newtonsoft.Json;
using Project_Backend_2024.Facade.FetchModels;

namespace Project_Backend_2024.Services.QueryServices.GithubUser;

public class GetGithubUserRepos : IRequestHandler<GetGithubRepos, GithubRepo[]>
{
    public async Task<GithubRepo[]> Handle(GetGithubRepos request, CancellationToken cancellationToken)
    {
        using var httpClient = new HttpClient();
    
        var getReposRequest = new HttpRequestMessage(HttpMethod.Get, request.ReposUrl);
        getReposRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", request.AccessToken);
        getReposRequest.Headers.UserAgent.ParseAdd("Bitasmbl"); 

        var repoResponse = await httpClient.SendAsync(getReposRequest, cancellationToken);
        
        var repoResponseBody = await repoResponse.Content.ReadAsStringAsync(cancellationToken);
        
        var deserializedRepos = JsonConvert.DeserializeObject<GithubRepo[]>(repoResponseBody)
                                ?? throw new SerializationException();

        return deserializedRepos;
    }
}