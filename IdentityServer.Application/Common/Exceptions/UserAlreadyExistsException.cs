namespace IdentityServer.Application.Common.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string username, string? email)
        : base($"User with username \"{username}\" or email \"{email}\" already exists.")
    {
    }
}