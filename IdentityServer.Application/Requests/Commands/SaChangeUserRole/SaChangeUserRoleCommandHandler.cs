using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Interfaces;
using IdentityServer.Domain.IdentityUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Application.Requests.Commands.SaChangeUserRole;

public class SaChangeUserRoleCommandHandler : IRequestHandler<SaChangeUserRoleCommand>
{
    private readonly IIdentityDbContext _context;

    public SaChangeUserRoleCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(SaChangeUserRoleCommand request, CancellationToken cancellationToken)
    {
        // try to get user with given username
        var user = await GetUserByUsernameAsync(request.Username, cancellationToken);

        // If user not found - throw exception
        if (user is null)
            throw new NotFoundException(nameof(IdentityUser), request.Username);
        
        var role = await GetRole(request.NewRoleName, cancellationToken);

        if (role is null)
            throw new NotFoundException(nameof(IdentityUserRole), request.NewRoleName);

        user.Role = role;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<IdentityUser?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(user => user.Personal)
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Username == username,
                cancellationToken);
    }

    // get role by name
    private async Task<IdentityUserRole?> GetRole(string roleName, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(role => role.Name == roleName, cancellationToken);
    }
}