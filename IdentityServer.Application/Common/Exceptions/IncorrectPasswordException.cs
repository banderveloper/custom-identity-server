namespace IdentityServer.Application.Common.Exceptions;

public class IncorrectPasswordException : Exception
{
    public IncorrectPasswordException(string username) : base($"Incorrect password to user \"{username}\"")
    {
    }
}