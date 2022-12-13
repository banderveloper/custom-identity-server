using IdentityServer.Domain.IdentityUser;

namespace IdentityServer.Persistence;

public static class DbInitializer
{
    public static void Initialize(IdentityDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Roles.Any())
        {
            context.Roles.Add(new IdentityUserRole { Name = "user" });
            context.Roles.Add(new IdentityUserRole { Name = "admin" });
            context.SaveChanges();
        }
    }
}