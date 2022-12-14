using AutoMapper;
using IdentityServer.Application.Common.Mappings;
using IdentityServer.Application.Requests.User.Commands.RegisterUser;

namespace IdentityServer.Api.Models.User;

public class RegisterUserModel : IMappable
{
    // Registration data
    public string Username { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    // Personal data
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RegisterUserModel, RegisterUserCommand>();
    }
}