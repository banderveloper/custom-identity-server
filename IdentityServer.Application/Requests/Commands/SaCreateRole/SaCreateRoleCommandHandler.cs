using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Interfaces;
using IdentityServer.Domain.IdentityUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Application.Requests.Commands.SaCreateRole;

public class SaCreateRoleCommandHandler : IRequestHandler<SaCreateRoleCommand>
{
    private readonly IIdentityDbContext _context;

    public SaCreateRoleCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(SaCreateRoleCommand request, CancellationToken cancellationToken)
    {
        // role name to lowercase 
        request.RoleName = request.RoleName.ToLower();
        
        // try to get existing role
        var existingRole = await _context.Roles
            .FirstOrDefaultAsync(role => role.Name == request.RoleName, cancellationToken);

        // it role already exists - throw exception
        if (existingRole is not null)
            throw new AlreadyExistsException(nameof(IdentityUserRole), existingRole.Name);

        // add new role and save
        _context.Roles.Add(new IdentityUserRole
        {
            Name = request.RoleName
        });
        
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}