using MediatR;
using static System.String;

namespace IdentityServer.Application.Requests.Queries.GetUserPublicData;

public class GetUserPublicDataQuery : IRequest<UserPublicDataDto>
{
    public string Username { get; set; } = Empty;
}