using System.Text.Json.Serialization;
using static System.String;

namespace IdentityServer.Domain.IdentityUser;

// Personal data or the user
public class IdentityUserPersonal
{
    public int Id { get; set; }

    public string? FirstName { get; set; } = Empty;
    public string? LastName { get; set; } = Empty;
    public string? PhoneNumber { get; set; } = Empty;
    
    [JsonIgnore] public IdentityUser? User { get; set; }
}