using MediatR;

namespace IdentityServer.Application.Requests.User.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<int>
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