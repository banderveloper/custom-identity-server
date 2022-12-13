using System.Text.Json.Serialization;

namespace IdentityServer.Domain.IdentityUser;

// Registration data of the user
public class IdentityUser
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    // Personal data (name, surname, ...)
    public int PersonalId { get; set; }
    [JsonIgnore] public IdentityUserPersonal? Personal { get; set; }

    // Role (user, admin, ...)
    public int RoleId { get; set; }
    [JsonIgnore] public IdentityUserRole? Role { get; set; }
}