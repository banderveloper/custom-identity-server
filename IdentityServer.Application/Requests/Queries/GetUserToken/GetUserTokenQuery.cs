using MediatR;
using static System.String;

namespace IdentityServer.Application.Requests.Queries.GetUserToken;

public class GetUserTokenQuery : IRequest<string>
{
   public string Username { get; init; } = Empty;
   public string Password { get; init; } = Empty;
}