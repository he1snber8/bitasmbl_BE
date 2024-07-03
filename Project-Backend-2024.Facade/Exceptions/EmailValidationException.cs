namespace Project_Backend_2024.Facade.Exceptions;

public class EmailValidationException(string entityMail) : Exception
{
    private string Mail { get; } = entityMail;
    public override string ToString()
    {
        return $"Entity with an email {Mail} already exists";
    }
}