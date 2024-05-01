using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Facade.Exceptions;

public class UsernameValidationException : Exception
{
    public UsernameValidationException(string message) : base(message) { }
}

public class PasswordValidationException : Exception
{
    public PasswordValidationException(string message) : base(message) { }
}

public class EmailValidationException : Exception
{
    public EmailValidationException(string message) : base(message) { }
}
