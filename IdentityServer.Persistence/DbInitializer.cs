namespace IdentityServer.Persistence;

public static class DbInitializer
{
    public static void Initialize(IdentityDbContext context)
    {
        context.Database.EnsureCreated();
    }
}