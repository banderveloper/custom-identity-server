using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Interfaces;
using IdentityServer.Domain.IdentityUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Application.Requests.Queries.SaGetUserData;

public class SaGetUserDataQueryHandler : IRequestHandler<SaGetUserDataQuery, IdentityUser>
{
    private readonly IIdentityDbContext _context;

    public SaGetUserDataQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<IdentityUser> Handle(SaGetUserDataQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(user => user.Personal)
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Username == request.Username, cancellationToken);

        if (user is null)
            throw new NotFoundException(nameof(IdentityUser), request.Username);

        return user;
    }
}