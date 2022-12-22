using MediatR;
using static System.String;

namespace IdentityServer.Application.Requests.Commands.SaChangeUserRole;

public class SaChangeUserRoleCommand : IRequest
{
    public string Username { get; set; } = Empty;
    public string NewRoleName { get; set; } = Empty;
}