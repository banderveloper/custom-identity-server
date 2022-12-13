using MediatR;

namespace IdentityServer.Application.Requests.SA.Commands.CreateAdmin;

public class CreateAdminCommand : IRequest
{
    // Registration data
    public string Username { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    // Personal data
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
}