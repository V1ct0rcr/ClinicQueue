using System.Security.Cryptography;
using System.Text;

namespace eUseControl.BusinessLayer.Structure;

public static class PasswordHasher
{
    private const string PasswordSuffix = "clinicqueue_2026";

    public static string Hash(string password)
    {
        var input = password + PasswordSuffix;
        var bytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = MD5.HashData(bytes);

        var sb = new StringBuilder();
        foreach (var b in hashBytes)
            sb.Append(b.ToString("x2"));

        return sb.ToString();
    }
}
