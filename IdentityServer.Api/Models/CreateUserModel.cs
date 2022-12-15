using System.ComponentModel.DataAnnotations;
using AutoMapper;
using IdentityServer.Application.Common.Mappings;
using IdentityServer.Application.Requests.Commands.CreateUser;

namespace IdentityServer.Api.Models;

public class CreateUserModel : IMappable
{
    // Required
    [Required] public string Username { get; set; }
    [Required] public string Password { get; set; }

    // Personal (not required)
    public string? FirstName { get; set; } = null;
    public string? LastName { get; set; } = null;
    public string? PhoneNumber { get; set; } = null;
    public string? Email { get; set; } = null;
    public string? Country { get; set; } = null;
    public int? Age { get; set; } = null;
    public string? Work { get; set; } = null;
    public string? WorkPost { get; set; } = null;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserModel, CreateUserCommand>();
    }
}