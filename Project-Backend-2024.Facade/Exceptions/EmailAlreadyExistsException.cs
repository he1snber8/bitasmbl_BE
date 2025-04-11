namespace Project_Backend_2024.Facade.Exceptions;

public class EmailAlreadyExistsException : Exception
{
    public EmailAlreadyExistsException(string email) 
        : base($"Email {email} already exists")
    {
    }

    public EmailAlreadyExistsException(string email, string registrationType)
        : base($"{email} already exists, logging in as {registrationType} user")
    {
    }
}

public class InsufficientFundsException() : Exception($"Insufficient funds, please fill up the balance");