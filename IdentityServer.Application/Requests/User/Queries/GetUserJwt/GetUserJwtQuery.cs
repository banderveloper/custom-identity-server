using MediatR;

namespace IdentityServer.Application.Requests.User.Queries.GetUserJwt;

public class GetUserJwtQuery : IRequest<string>
{
    public int UserId { get; set; }
}