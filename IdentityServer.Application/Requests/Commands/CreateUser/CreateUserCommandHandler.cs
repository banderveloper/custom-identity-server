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
        // Try to get user with given username in request
        var existingUser = await GetUserByUsernameAsync(request.Username, cancellationToken);

        // If already exists user with given username - throw exception
        if (existingUser is not null)
            throw new AlreadyExistsException(nameof(existingUser), existingUser.Username);

        // Try to get user role, to give it for new user
        var userRole = await GetUserRoleAsync(cancellationToken);

        // If user role does not exists (hypothetically might not happen) - throw exception
        if (userRole is null)
            throw new NotFoundException(nameof(userRole), _roleConfiguration.UserRole);
        
        // If everything is ok - start creating user
        
        // Create user personal data from request 
        var personal = GetUserPersonalFromRequest(request);
        
        // Create user with personal and user role
        var user = new IdentityUser
        {
            Username = request.Username,
            PasswordHash = Sha256.Hash(request.Password),
            Personal = personal,
            RoleId = userRole.Id
        };
        
        // Save it to database
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        
        // Return id of created user
        return user.Id;
    }

    private IdentityUserPersonal GetUserPersonalFromRequest(CreateUserCommand request)
    {
        return new IdentityUserPersonal
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
    }

    private async Task<IdentityUser?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(user => user.Username == username,
                cancellationToken);
    }

    private async Task<IdentityUserRole?> GetUserRoleAsync(CancellationToken cancellationToken)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(role => role.Name == _roleConfiguration.UserRole,
                cancellationToken);
    }
}