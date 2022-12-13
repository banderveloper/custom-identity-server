using System.Text;

namespace IdentityServer.Application.Common.Hashing;

public static class Sha256
{
    public static string Hash(string origin)
    {
        var crypt = new System.Security.Cryptography.SHA256Managed();
        var hash = new System.Text.StringBuilder();
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(origin));
        foreach (var theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }

        return hash.ToString();
    }
}