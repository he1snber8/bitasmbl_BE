namespace Project_Backend_2024.Facade.Exceptions;

public class UserLoginException : Exception
{
    public string? Username { get; }
    public bool IsPasswordIncorrect { get; }
    public UserLoginException() : base("You have entered an invalid username or password") {}

    public UserLoginException(string? username) 
        : base($"Invalid login attempt for username '{username}'")
    {
        Username = username;
        IsPasswordIncorrect = false;
    }

    public UserLoginException(string? username, bool isPasswordIncorrect) 
        : base(isPasswordIncorrect 
            ? $"Incorrect password for username '{username}'" 
            : $"Invalid login attempt for username '{username}'")
    {
        Username = username;
        IsPasswordIncorrect = isPasswordIncorrect;
    }

    public UserLoginException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }

    public override string ToString()
    {
        return Message;
    }
}
