using MediatR;

namespace IdentityServer.Application.Requests.SA.Commands.CreateRole;

public class CreateRoleCommand : IRequest
{
    public string Name { get; set; } = string.Empty;
}