using AutoMapper;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;

namespace Harmonify.Server.Mappers;

public class ApplicationUserMapper : Profile
{
    public ApplicationUserMapper()
    {
        CreateMap<ApplicationUser, ApplicationUserDTO>()
            .ForMember(d => d.Id,
                m => m.MapFrom(s => s.Id))
            .ForMember(d => d.FullName,
                m => m.MapFrom(s => s.FirstName + " " + s.LastName))
            .ForMember(d => d.FirstName,
                m => m.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName,
                m => m.MapFrom(s => s.LastName))
            .ForMember(d => d.Description,
                m => m.MapFrom(s => $"Lives in {s.Address.City} | Joined in {s.JoinedOn.Year}"))
            .ForMember(d => d.Email,
                m => m.MapFrom(s => s.Email))
            .ForMember(d => d.City,
                m => m.MapFrom(s => s.Address.City))
            .ForMember(d => d.Birthday,
                m => m.MapFrom(s => s.Birthday.HasValue ? s.Birthday.Value.ToString("dd/MM/yyyy") : null))
            .ForMember(d => d.ProfileUrl,
                m => m.MapFrom(s => $"/Profile/{s.Id}"))
            .ForMember(d => d.JoinedOn,
                m => m.MapFrom(s => s.JoinedOn.ToString("yyyy")))
            .ForMember(d => d.AvatarContent,
                m => m.MapFrom(s => s.Avatar != null ? s.Avatar.Content : null))
            .ForMember(d => d.AvatarFileName,
                m => m.MapFrom(s => s.Avatar != null ? s.Avatar.FileName : null))
            .ForMember(d => d.AvatarSource,
                m => m.MapFrom(s => s.Avatar != null ? $"data:image/jpg;base64, {Convert.ToBase64String(s.Avatar.Content)}" : "unknown-avatar.png"));
    }
}