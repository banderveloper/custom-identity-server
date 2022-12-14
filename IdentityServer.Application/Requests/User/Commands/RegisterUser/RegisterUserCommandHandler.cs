using IdentityServer.Application.Common.Configurations;
using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Common.Hashing;
using IdentityServer.Application.Interfaces;
using IdentityServer.Domain.IdentityUser;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IdentityServer.Application.Requests.User.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
{
    private readonly IIdentityDbContext _context;
    private readonly DefaultRoleConfiguration _roleConfiguration;
    
    public RegisterUserCommandHandler(IIdentityDbContext context, DefaultRoleConfiguration roleConfiguration)
    {
        _context = context;
        _roleConfiguration = roleConfiguration;
        Console.WriteLine("DEFAULT ROLE: " + _roleConfiguration.UserRole);
    }
    
    public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Try to get user with given username or email
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(user =>
                    user.Username == request.Username || user.Email == request.Email,
                cancellationToken);

        // If it is already exists - ex
        if (existingUser is not null)
            throw new UserAlreadyExistsException(request.Username, request?.Email);

        // If its ok
        var userRole = await _context.Roles
            .FirstOrDefaultAsync(role => role.Name == _roleConfiguration.UserRole, cancellationToken);

        var newUser = new IdentityUser
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = Sha256.Hash(request.Password),
            RoleId = userRole.Id,
            Personal = new IdentityUserPersonal
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber
            }
        };
        
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync(cancellationToken);

        return newUser.Id;
    }
}