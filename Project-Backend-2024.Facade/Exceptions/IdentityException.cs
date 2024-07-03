using Microsoft.AspNetCore.Identity;

namespace Project_Backend_2024.Facade.Exceptions;

public class IdentityException(IEnumerable<IdentityError> errors) : Exception("Identity operation failed.")
{
    private IEnumerable<IdentityError> Errors { get; } = errors;

    public override string ToString()
    {
        var errorDescriptions = string.Join("; ", Errors.Select(e => e.Description));
        return $"{Message} Errors: {errorDescriptions}";
    }
}