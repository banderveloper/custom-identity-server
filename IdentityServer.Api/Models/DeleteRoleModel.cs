using System.ComponentModel.DataAnnotations;
using AutoMapper;
using IdentityServer.Application.Common.Mappings;
using IdentityServer.Application.Requests.Commands.SaDeleteRole;

namespace IdentityServer.Api.Models;

public class DeleteRoleModel : IMappable
{
    [Required] public string RoleName { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<DeleteRoleModel, SaDeleteRoleCommand>();
    }
}