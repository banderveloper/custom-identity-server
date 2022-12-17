using System.Threading.Tasks.Sources;
using IdentityServer.Application.Common.Services;
using IdentityServer.Domain.IdentityUser;
using IdentityServer.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Tests.Common;

public class IdentityContextFactory
{
    public static string UsernameA => "usernameA";
    public static string UsernameB => "usernameB";

    public static string PasswordA => "passwordA";
    public static string PasswordB => "passwordB";

    public static string UserRole => "user";
    public static string SecondRole => "teacher";


    public static IdentityDbContext Create()
    {
        // Init db context options
        var options = new DbContextOptionsBuilder<IdentityDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        // Create in memory db context using initialized options 
        var context = new IdentityDbContext(options);
        context.Database.EnsureCreated();
        
        // Add test roles and users to context 
        AddRolesToContext(context);
        AddUsersToContext(context);
        
        context.SaveChanges();

        return context;
    }

    private static void AddRolesToContext(IdentityDbContext context)
    {
        context.Roles.Add(new IdentityUserRole { Id = 1, Name = UserRole });
        context.Roles.Add(new IdentityUserRole { Id = 2, Name = SecondRole });
    }

    private static void AddUsersToContext(IdentityDbContext context)
    {
        context.Users.Add(new IdentityUser
        {
            Id = 1,
            Username = UsernameA,
            PasswordHash = Sha256.Hash(PasswordA),
            RoleId = 1,
            Personal = new IdentityUserPersonal()
            {
                FirstName = "FirstNameA",
                LastName = "LastNameA"
            }
        });

        context.Users.Add(new IdentityUser
        {
            Id = 2,
            Username = UsernameB,
            PasswordHash = Sha256.Hash(PasswordB),
            RoleId = 2,
            Personal = new IdentityUserPersonal()
            {
                FirstName = "FirstNameB",
                LastName = "LastNameB"
            }
        });
    }

    public static void Destroy(IdentityDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}