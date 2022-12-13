using System.Text.Json.Serialization;

namespace IdentityServer.Domain.IdentityUser;

// Role of the user (user, moderator, admin, ...)
public class IdentityUserRole
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    [JsonIgnore] public IEnumerable<IdentityUser> Users { get; set; }
}