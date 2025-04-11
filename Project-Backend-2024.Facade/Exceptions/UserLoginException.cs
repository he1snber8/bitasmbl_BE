namespace Project_Backend_2024.Facade.Exceptions;

public class UserLoginException : Exception
{
    public string? UserIdentificator { get; }
    public bool IsPasswordIncorrect { get; }
    public UserLoginException() : base("You have entered an invalid username or password") {}

    public UserLoginException(string? userIdentificator) 
        : base($"user with '{userIdentificator}' is not yet registered")
    {
        UserIdentificator = userIdentificator;
        IsPasswordIncorrect = false;
    }

    public UserLoginException(string? userIdentificator, bool isPasswordIncorrect) 
        : base(isPasswordIncorrect 
            ? $"Incorrect password for '{userIdentificator}'" 
            : $"Invalid login attempt")
    {
        UserIdentificator = userIdentificator;
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
