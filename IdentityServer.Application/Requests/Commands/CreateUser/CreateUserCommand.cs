using MediatR;
using static System.String;

namespace IdentityServer.Application.Requests.Commands.CreateUser;

// CQRS command for CreateUserCommandHandler
public class CreateUserCommand : IRequest
{
    // Required
    public string Username { get; set; } = Empty;
    public string Password { get; set; } = Empty;

    // Personal (not required)
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Country { get; set; }
    public int? Age { get; set; }
    public string? Work { get; set; }
    public string? WorkPost { get; set; }
}