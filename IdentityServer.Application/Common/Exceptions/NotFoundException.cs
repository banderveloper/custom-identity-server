namespace IdentityServer.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public object NotFoundValue { get; }

    public NotFoundException(string name, object key)
        : base($"Entity <{name}> with value \"{key}\" not found.")
    {
        NotFoundValue = key;
    }
}