using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityServer.Application.Common.Configurations;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Application.Requests.Queries.GetUserToken;

public class GetUserTokenQueryHandler : IRequestHandler<GetUserTokenQuery, string>
{
    private readonly JwtConfiguration _jwtConfiguration;

    public GetUserTokenQueryHandler(JwtConfiguration jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration;
    }

    public async Task<string> Handle(GetUserTokenQuery request, CancellationToken cancellationToken)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, request.PublicData.Username),
            new Claim(ClaimTypes.Role, request.PublicData.Role),
            new Claim(ClaimTypes.Name, request.PublicData.FirstName),
            new Claim(ClaimTypes.Surname, request.PublicData.LastName),
            new Claim(ClaimTypes.Email, request.PublicData.Email),
            new Claim(ClaimTypes.MobilePhone, request.PublicData.PhoneNumber),
            new Claim("age", request.PublicData.Age?.ToString()),
            new Claim("job", request.PublicData.Work),
            new Claim("jobPost", request.PublicData.WorkPost),
            new Claim("country", request.PublicData.Country)
        };
        var token = new JwtSecurityToken(_jwtConfiguration.Issuer,
            _jwtConfiguration.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtConfiguration.TTL),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}