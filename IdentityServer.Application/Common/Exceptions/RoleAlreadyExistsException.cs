namespace IdentityServer.Application.Common.Exceptions;

public class RoleAlreadyExistsException : Exception
{
    public RoleAlreadyExistsException(string roleName)
        : base($"Role \"{roleName}\" already exists.")
    {
    }
}