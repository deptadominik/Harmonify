using AutoMapper;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;

namespace Harmonify.Server.Mappers;

public class PostMapper : Profile
{
    public PostMapper()
    {
        CreateMap<Post, PostDTO>()
            .ForMember(p => p.Id,
                m => m.MapFrom(s => s.Id))
            .ForMember(p => p.Content,
                m => m.MapFrom(s => s.Content))
            .ForMember(p => p.Type,
                m => m.MapFrom(s => s.Type))
            .ForMember(p => p.Images,
                m => m.MapFrom(s => s.Images))
            .ForMember(p => p.PostedAt,
                m => m.MapFrom(s => s.PostedAt))
            .ForMember(p => p.EditedAt,
                m => m.MapFrom(s => s.EditedAt))
            .ForMember(p => p.Author,
                m => m.MapFrom(s => s.Author))
            .ForMember(p => p.Comments,
                m => m.MapFrom(s => s.Comments));
    }
}