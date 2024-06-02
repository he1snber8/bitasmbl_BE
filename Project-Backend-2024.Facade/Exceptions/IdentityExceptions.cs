using Microsoft.AspNetCore.Identity;

namespace Project_Backend_2024.Facade.Exceptions;

public class PasswordValidationException : Exception
{
    public PasswordValidationException(string message) : base(message) { }
}

public class IdentityException : Exception
{
    public IEnumerable<IdentityError> Errors { get; }

    public IdentityException(IEnumerable<IdentityError> errors)
        : base("Identity operation failed.")
    {
        Errors = errors;
    }

    public override string ToString()
    {
        var errorDescriptions = string.Join("; ", Errors.Select(e => e.Description));
        return $"{Message} Errors: {errorDescriptions}";
    }
}

public class EmailValidationException : Exception
{
    public string Mail { get; private set; }

    public EmailValidationException(string EntityMail)
    {
        Mail = EntityMail;
    }

    public override string ToString()
    {
        return $"Entity with an email {Mail} already exists";
    }
}

public class UserNotFoundException : Exception
{
    public override string ToString()
    {
        return $"Entity does not exist";
    }
}

public class UserLoginException : Exception
{
    public override string ToString() => "You have entered an invalid username or password";
}

public class TokenValidationException : Exception
{
    public TokenValidationException(string message):base(message) { }
}
