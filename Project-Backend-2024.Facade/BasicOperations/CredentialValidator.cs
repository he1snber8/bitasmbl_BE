
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Facade.BasicOperations;

public static class CredentialValidator
{
    public static bool ValidatePassword(this IAuthenticatable entity) =>
            entity.Password!.Length > 7 && entity.Password!.Any(char.IsUpper) && entity.Password!.Any(char.IsDigit);


    public static bool ValidateEmail(this IMailApplicable entity) =>
            entity.Email!.Length > 8 || entity.Email!.Contains('@');

    public static bool ValidateUsername(this IAuthenticatable entity) =>
            entity.Username!.Length > 5;

}
