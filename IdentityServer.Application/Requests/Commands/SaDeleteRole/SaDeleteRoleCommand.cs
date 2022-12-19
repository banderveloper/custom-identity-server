using MediatR;
using static System.String;

namespace IdentityServer.Application.Requests.Commands.SaDeleteRole;

public class SaDeleteRoleCommand : IRequest
{
    public string RoleName { get; set; } = Empty;
}