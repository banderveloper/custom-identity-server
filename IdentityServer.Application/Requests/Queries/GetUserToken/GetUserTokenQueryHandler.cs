using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityServer.Application.Common.Configurations;
using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Common.Services;
using IdentityServer.Application.Interfaces;
using IdentityServer.Domain.IdentityUser;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Application.Requests.Queries.GetUserToken;

public class GetUserTokenQueryHandler : IRequestHandler<GetUserTokenQuery, string>
{
    private readonly IIdentityDbContext _context;
    private readonly JwtConfiguration _jwtConfiguration;

    public GetUserTokenQueryHandler(IIdentityDbContext context, JwtConfiguration jwtConfiguration)
    {
        _context = context;
        _jwtConfiguration = jwtConfiguration;
    }

    public async Task<string> Handle(GetUserTokenQuery request, CancellationToken cancellationToken)
    {
        // try to get user with given username
        var user = await GetUserByUsernameAsync(request.Username, cancellationToken);

        // If user not found - throw exception
        if (user is null)
            throw new NotFoundException(nameof(user), request.Username);

        // If user found but password is incorrect
        if (user.PasswordHash != Sha256.Hash(request.Password))
            throw new IncorrectPasswordException(user.Username);

        // generate token
        var token = GenerateToken(user.Username, user.Role.Name);

        return token;
    }

    private string GenerateToken(string username, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Role, role)
        };
        var token = new JwtSecurityToken(_jwtConfiguration.Issuer,
            _jwtConfiguration.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtConfiguration.TTL),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<IdentityUser?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Username == username,
                cancellationToken);
    }
}