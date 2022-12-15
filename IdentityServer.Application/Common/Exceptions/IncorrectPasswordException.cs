namespace IdentityServer.Application.Common.Exceptions;

public class IncorrectPasswordException : Exception
{
    public string Username { get; }

    public IncorrectPasswordException(string username)
        : base($"Incorrect password from user \"{username}\"")
    {
        Username = username;
    }
}