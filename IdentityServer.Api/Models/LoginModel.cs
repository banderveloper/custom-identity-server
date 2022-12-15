using System.ComponentModel.DataAnnotations;
using AutoMapper;
using IdentityServer.Application.Common.Mappings;
using IdentityServer.Application.Requests.Queries.GetUserPublicData;

namespace IdentityServer.Api.Models;

public class LoginModel : IMappable
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginModel, GetUserPublicDataQuery>();
    }
}