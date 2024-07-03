namespace Project_Backend_2024.Facade.Exceptions;

public class PasswordValidationException(string message) : Exception
{
    public override string ToString() => message;

}