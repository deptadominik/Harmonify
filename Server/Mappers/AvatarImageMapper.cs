using AutoMapper;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;

namespace Harmonify.Server.Mappers;

public class AvatarImageMapper : Profile
{
    public AvatarImageMapper()
    {
        CreateMap<AvatarImage, AvatarImageDTO>()
            .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
            .ForMember(d => d.Content, m => m.MapFrom(s => s.Content))
            .ForMember(d => d.FileName, m => m.MapFrom(s => s.FileName))
            .ForMember(d => d.UserId, m => m.MapFrom(s => s.UserId));
    }
}