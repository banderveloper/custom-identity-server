using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Interfaces;
using IdentityServer.Domain.IdentityUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Application.Requests.SA.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
{
    private readonly IIdentityDbContext _context;

    public CreateRoleCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        // Check if given role doesn't exists
        var role = await _context.Roles
            .FirstOrDefaultAsync(role => role.Name == request.Name.ToLower(),
                cancellationToken);

        // if role already exists - throw ex
        if (role is not null)
            throw new RoleAlreadyExistsException(role.Name);

        // if not - push new role to db
        _context.Roles.Add(new IdentityUserRole { Name = request.Name.ToLower() });
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}