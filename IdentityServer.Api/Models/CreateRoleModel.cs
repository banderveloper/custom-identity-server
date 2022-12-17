using System.ComponentModel.DataAnnotations;
using AutoMapper;
using IdentityServer.Application.Common.Mappings;
using IdentityServer.Application.Requests.Commands.SaCreateRole;

namespace IdentityServer.Api.Models;

public class CreateRoleModel : IMappable
{
    [Required] public string RoleName { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateRoleModel, SaCreateRoleCommand>();
    }
}