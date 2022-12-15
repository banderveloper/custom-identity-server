using IdentityServer.Application.Common.Configurations;
using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Common.Hashing;
using IdentityServer.Application.Interfaces;
using IdentityServer.Domain.IdentityUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Application.Requests.Commands.CreateUser;

// Creates user at database and returns his id
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IIdentityDbContext _context;
    private readonly DefaultRoleConfiguration _roleConfiguration;

    public CreateUserCommandHandler(IIdentityDbContext context, DefaultRoleConfiguration roleConfiguration)
    {
        _context = context;
        _roleConfiguration = roleConfiguration;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Try to get existing user with given username
        var existingUser = await _context.Users
            .FindAsync(new object[] { request.Username }, cancellationToken);

        // If database already has user with given username - throw exception
        if (existingUser is not null)
            throw new AlreadyExistsException(nameof(existingUser), existingUser.Username);

        // If not - register it

        // Get USER role
        var userRole = await _context.Roles
            .FindAsync(new object[] { _roleConfiguration.UserRole }, cancellationToken);
        // Error if role not found (hypothetically cannot happen, but let it be) 
        if (userRole is null)
            throw new NotFoundException(nameof(userRole), _roleConfiguration.UserRole);

        // Create user personals, it will be added to user
        var userPersonal = new IdentityUserPersonal
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Age = request.Age,
            Country = request.Country,
            Email = request.Email,
            Work = request.Work,
            PhoneNumber = request.PhoneNumber,
            WorkPost = request.WorkPost
        };

        // Create user entity with connected personal and user role
        var user = new IdentityUser
        {
            Username = request.Username,
            PasswordHash = Sha256.Hash(request.Password),
            Personal = userPersonal,
            RoleId = userRole.Id
        };

        // Save to database
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        // Return user given id
        return user.Id;
    }
}