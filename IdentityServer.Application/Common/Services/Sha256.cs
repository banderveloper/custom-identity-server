using System.Security.Cryptography;
using System.Text;

namespace IdentityServer.Application.Common.Services;

public static class Sha256
{
    public static string Hash(string origin)
    {
        var crypt = new SHA256Managed();
        var hash = new StringBuilder();
        var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(origin));
        foreach (var theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }

        return hash.ToString();
    }
}