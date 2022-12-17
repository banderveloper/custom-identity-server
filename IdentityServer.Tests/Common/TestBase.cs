using IdentityServer.Application.Common.Configurations;
using IdentityServer.Persistence;

namespace IdentityServer.Tests.Common;

public class TestBase : IDisposable
{
    protected readonly IdentityDbContext Context;
    protected readonly DefaultRoleConfiguration RoleConfiguration;

    public TestBase()
    {
        Context = IdentityContextFactory.Create();
        RoleConfiguration = new DefaultRoleConfiguration()
        {
            UserRole = "user",
            AdminRole = "admin"
        };
    }

    public void Dispose()
    {
        IdentityContextFactory.Destroy(Context);
    }
}