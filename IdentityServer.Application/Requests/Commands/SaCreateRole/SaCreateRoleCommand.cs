using MediatR;
using static System.String;

namespace IdentityServer.Application.Requests.Commands.SaCreateRole;

public class SaCreateRoleCommand : IRequest
{
    public string RoleName { get; set; } = Empty;
}