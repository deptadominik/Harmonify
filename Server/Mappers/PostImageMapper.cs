using AutoMapper;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;

namespace Harmonify.Server.Mappers;

public class PostImageMapper : Profile
{
    public PostImageMapper()
    {
        CreateMap<PostImageDTO, PostImage>()
            .ForMember(p => p.Id,
                m => m.MapFrom(s => s.Id))
            .ForMember(p => p.Name, m => m.MapFrom(p => p.Name))
            .ForMember(p => p.Url, m => m.MapFrom(p => p.Url))
            .ForMember(p => p.PostId, m => m.MapFrom(p => p.PostId));
    }
}