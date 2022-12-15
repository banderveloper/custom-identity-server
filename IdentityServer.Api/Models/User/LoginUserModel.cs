using AutoMapper;
using IdentityServer.Application.Common.Mappings;
using IdentityServer.Application.Requests.User.Queries.LoginUser;

namespace IdentityServer.Api.Models.User;

public class LoginUserModel : IMappable
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginUserModel, LoginUserQuery>();
    }
}