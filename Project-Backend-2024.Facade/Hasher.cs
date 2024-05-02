using System.Security.Cryptography;
using System.Text;

namespace Project_Backend_2024.Facade;

public static class Hasher
{
    public static string HashData(this string plainTextPassword)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainTextPassword));
            return Convert.ToBase64String(hashBytes);
        }
    }

    public static bool HashEquals(this string thisHash, string otherHash)
        => thisHash.HashData() == otherHash;

}
