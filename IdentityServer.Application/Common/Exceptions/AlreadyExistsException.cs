namespace IdentityServer.Application.Common.Exceptions;

public class AlreadyExistsException : Exception
{
    public object ExistingValue { get; }

    public AlreadyExistsException(string name, object key)
        : base($"Entity <{name}> with value \"{key}\" already exists in database.")
    {
        ExistingValue = key;
    }
}