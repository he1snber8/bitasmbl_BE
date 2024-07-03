namespace Project_Backend_2024.Facade.Exceptions;

public class UserLockedException : Exception
{
    private string? Username { get; }
    public UserLockedException() : base("User does not exist") {}
    
    public UserLockedException(string? username) 
        : base($"Account {username} is locked out")
    {
        Username = username;
    }
    
    public override string Message => Username is null ? base.ToString() : $"Account is locked, please contact my ass";
}