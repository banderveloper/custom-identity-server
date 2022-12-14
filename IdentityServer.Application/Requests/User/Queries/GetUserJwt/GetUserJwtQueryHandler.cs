using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Interfaces;
using IdentityServer.Domain.IdentityUser;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Application.Requests.User.Queries.GetUserJwt;

public class GetUserJwtQueryHandler : IRequestHandler<GetUserJwtQuery, string>
{
    private readonly IIdentityDbContext _context;
    private readonly IConfiguration _configuration;

    public GetUserJwtQueryHandler(IIdentityDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string> Handle(GetUserJwtQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(user => user.Personal)
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Id == request.UserId);

        if(user is null)
            throw new UserNotFoundException();

        var token = GenerateToken(user);
        return token;
    }

    private string GenerateToken(IdentityUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Name),
            new Claim(ClaimTypes.Name, user.Personal.FirstName),
            new Claim(ClaimTypes.Surname, user.Personal.LastName),
            new Claim(ClaimTypes.MobilePhone, user.Personal.PhoneNumber),
        };
        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:TTL"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}