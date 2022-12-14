using IdentityServer.Domain.IdentityUser;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Persistence;

public static class DbInitializer
{
    public static void Initialize(IdentityDbContext context, IConfiguration config)
    {
        context.Database.EnsureCreated();

        // Create default roles if they are not exists
        if (!context.Roles.Any())
        {
            context.Roles.Add(new IdentityUserRole { Name = config["DefaultRoles:User"] });
            context.Roles.Add(new IdentityUserRole { Name = config["DefaultRoles:Admin"] });
            context.SaveChanges();
        }
    }
}