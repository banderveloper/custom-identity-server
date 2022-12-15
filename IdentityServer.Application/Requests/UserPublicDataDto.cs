using AutoMapper;
using IdentityServer.Application.Common.Mappings;
using IdentityServer.Domain.IdentityUser;
using static System.String;

namespace IdentityServer.Application.Requests;

public class UserPublicDataDto : IMappable
{
    public string Username { get; set; } = Empty;
    public string Role { get; set; } = Empty;

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
        profile.CreateMap<IdentityUser, UserPublicDataDto>()
            .ForMember(dto => dto.Username, options =>
                options.MapFrom(user => user.Username))
            .ForMember(dto => dto.Role, options =>
                options.MapFrom(user => user.Role.Name))
            .ForMember(dto => dto.FirstName, options =>
                options.MapFrom(user => user.Personal.FirstName))
            .ForMember(dto => dto.LastName, options =>
                options.MapFrom(user => user.Personal.LastName))
            .ForMember(dto => dto.Age, options =>
                options.MapFrom(user => user.Personal.Age))
            .ForMember(dto => dto.Country, options =>
                options.MapFrom(user => user.Personal.Country))
            .ForMember(dto => dto.Email, options =>
                options.MapFrom(user => user.Personal.Email))
            .ForMember(dto => dto.Work, options =>
                options.MapFrom(user => user.Personal.Work))
            .ForMember(dto => dto.WorkPost, options =>
                options.MapFrom(user => user.Personal.WorkPost))
            .ForMember(dto => dto.PhoneNumber, options =>
                options.MapFrom(user => user.Personal.PhoneNumber));
    }
}