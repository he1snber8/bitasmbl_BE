using System.Text.RegularExpressions;

namespace Project_Backend_2024.Services.Formatting;

public static class EmailFormatValidator
{
    private static readonly Regex EmailRegex = new (
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static bool IsValidEmail(string email)
    {
        return EmailRegex.IsMatch(email);
        
    }
}