namespace IdentityServer.Application.Common.Exceptions;

public sealed class IncorrectPasswordException : Exception
{
    public string Username { get; }

    public IncorrectPasswordException(string username)
        : base($"Incorrect password from user with username '{username}'")
    {
        Username = username;
    }
}