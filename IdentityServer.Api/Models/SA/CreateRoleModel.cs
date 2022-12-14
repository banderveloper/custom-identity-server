using AutoMapper;
using IdentityServer.Application.Common.Mappings;
using IdentityServer.Application.Requests.SA.Commands.CreateRole;

namespace IdentityServer.Api.Models.SA;

public class CreateRoleModel : IMappable
{
    public string Name { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateRoleModel, CreateRoleCommand>()
            .ForMember(cmd => cmd.Name, options =>
                options.MapFrom(model => model.Name));
    }
}