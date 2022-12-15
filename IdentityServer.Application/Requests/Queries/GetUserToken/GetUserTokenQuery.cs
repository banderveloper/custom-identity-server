using MediatR;

namespace IdentityServer.Application.Requests.Queries.GetUserToken;

public class GetUserTokenQuery : IRequest<string>
{
   public string Username { get; set; }
   public string Password { get; set; }
}