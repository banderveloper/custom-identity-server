using MediatR;

namespace IdentityServer.Application.Requests.User.Queries.LoginUser;

public class LoginUserQuery : IRequest<int>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}