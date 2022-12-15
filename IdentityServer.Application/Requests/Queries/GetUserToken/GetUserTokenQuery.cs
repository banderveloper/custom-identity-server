using MediatR;

namespace IdentityServer.Application.Requests.Queries.GetUserToken;

public class GetUserTokenQuery : IRequest<string>
{
    public UserPublicDataDto PublicData { get; set; }
}