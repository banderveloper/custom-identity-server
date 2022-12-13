namespace IdentityServer.Domain.IdentityUser;

// Role of the user (user, moderator, admin, ...)
public class IdentityUserRole
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}