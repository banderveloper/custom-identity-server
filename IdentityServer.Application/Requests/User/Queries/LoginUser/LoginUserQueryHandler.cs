using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Common.Hashing;
using IdentityServer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Application.Requests.User.Queries.LoginUser;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, int>
{
    private readonly IIdentityDbContext _context;

    public LoginUserQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(user => user.Personal)
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Username == request.Username, cancellationToken);

        // if user not found
        if (user is null)
            throw new UserNotFoundException();

        // user found but incorrect password
        if (user.PasswordHash != Sha256.Hash(request.Password))
            throw new IncorrectPasswordException(user.Username);

        return user.Id;
    }
}