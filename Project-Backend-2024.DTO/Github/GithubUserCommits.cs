namespace Project_Backend_2024.DTO.Github;

public class GithubCommit
{
    public string Sha { get; set; }
    public CommitInfo Commit { get; set; }
    public GithubUser Author { get; set; }
    public GithubUser Committer { get; set; }
    public ParentCommit[] Parents { get; set; }
}

public class CommitInfo
{
    public CommitAuthor Author { get; set; }
    public CommitAuthor Committer { get; set; }
    public string Message { get; set; }
}

public class CommitAuthor
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Date { get; set; }
}

public class ParentCommit
{
    public string Sha { get; set; }
    public string Url { get; set; }
}
