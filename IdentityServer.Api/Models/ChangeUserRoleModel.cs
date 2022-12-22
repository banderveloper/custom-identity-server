using System.ComponentModel.DataAnnotations;
using AutoMapper;
using IdentityServer.Application.Common.Mappings;
using IdentityServer.Application.Requests.Commands.SaChangeUserRole;

namespace IdentityServer.Api.Models;

public class ChangeUserRoleModel : IMappable
{
    [Required] public string Username { get; set; }
    [Required] public string NewRoleName { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<ChangeUserRoleModel, SaChangeUserRoleCommand>();
    }
}