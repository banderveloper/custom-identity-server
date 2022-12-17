using IdentityServer.Domain.IdentityUser;
using MediatR;
using static System.String;

namespace IdentityServer.Application.Requests.Queries.SaGetUserData;

public class SaGetUserDataQuery : IRequest<IdentityUser>
{
    public string Username { get; init; } = Empty;
}