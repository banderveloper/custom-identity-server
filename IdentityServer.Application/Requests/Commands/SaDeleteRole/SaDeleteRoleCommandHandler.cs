using IdentityServer.Application.Common.Configurations;
using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Interfaces;
using IdentityServer.Domain.IdentityUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Application.Requests.Commands.SaDeleteRole;

public class SaDeleteRoleCommandHandler : IRequestHandler<SaDeleteRoleCommand>
{
    private readonly IIdentityDbContext _context;
    private readonly DefaultRoleConfiguration _roleConfiguration;

    public SaDeleteRoleCommandHandler(IIdentityDbContext context, DefaultRoleConfiguration roleConfiguration)
    {
        _context = context;
        _roleConfiguration = roleConfiguration;
    }

    public async Task<Unit> Handle(SaDeleteRoleCommand request, CancellationToken cancellationToken)
    {
        // get default user role
        var userRole = await GetUserRole(cancellationToken);

        // if user role not found - ex
        if (userRole is null)
            throw new NotFoundException(nameof(IdentityUserRole), _roleConfiguration.UserRole);

        // before deleting role - change role to default at users who have this role
        var usersWithDeletingRole = await GetUsersWithRole(request.RoleName, cancellationToken);

        foreach (var user in usersWithDeletingRole)
            user.Role = userRole;
        
        // get deleting role and delete
        var roleForDelete = await GetRole(request.RoleName, cancellationToken);
        
        if (roleForDelete is not null)
            _context.Roles.Remove(roleForDelete);
        
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<List<IdentityUser>> GetUsersWithRole(string role, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(user => user.Role.Name == role)
            .ToListAsync(cancellationToken);
    }

    // get role with name USER
    private async Task<IdentityUserRole?> GetUserRole(CancellationToken cancellationToken)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(role => role.Name == _roleConfiguration.UserRole,
                cancellationToken);
    }

    // get role by name
    private async Task<IdentityUserRole?> GetRole(string roleName, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(role => role.Name == roleName, cancellationToken);
    }
}