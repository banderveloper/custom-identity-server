using IdentityServer.Application.Requests.Queries.GetUserPublicData;
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
    public string? FirstName { get; set; } = null;
    public string? LastName { get; set; } = null;
    public string? PhoneNumber { get; set; } = null;
    public string? Email { get; set; } = null;
    public string? Country { get; set; } = null;
    public int? Age { get; set; } = null;
    public string? Work { get; set; } = null;
    public string? WorkPost { get; set; } = null;
}