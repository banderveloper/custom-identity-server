using IdentityServer.Application.Common.Configurations;
using IdentityServer.Domain.IdentityUser;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Persistence;

public static class DbInitializer
{
    public static void Initialize(IdentityDbContext context, DefaultRoleConfiguration roleConfiguration)
    {
        context.Database.EnsureCreated();

        // Create default roles if they are not exists
        if (!context.Roles.Any())
        {
            context.Roles.Add(new IdentityUserRole { Name = roleConfiguration.UserRole });
            context.Roles.Add(new IdentityUserRole { Name = roleConfiguration.AdminRole });
            context.SaveChanges();
        }
    }
}