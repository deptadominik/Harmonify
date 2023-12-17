using AutoMapper;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;

namespace Harmonify.Server.Mappers;

public class CommentMapper : Profile
{
    public CommentMapper()
    {
        CreateMap<Comment, CommentDTO>()
            .ForMember(p => p.Id,
                m => m.MapFrom(s => s.Id))
            .ForMember(p => p.Content,
                m => m.MapFrom(s => s.Content))
            .ForMember(p => p.PostedAt,
                m => m.MapFrom(s => s.PostedAt))
            .ForMember(p => p.EditedAt,
                m => m.MapFrom(s => s.EditedAt))
            .ForMember(p => p.Author,
                m => m.MapFrom(s => s.Author))
            .ForMember(p => p.Replies,
                m => m.MapFrom(s => s.Replies))
            .ForMember(p => p.Likes,
                m => m.MapFrom(s => s.Likes))
            .ForMember(p => p.Post,
                m => m.MapFrom(s => s.Post))
            .ForMember(p => p.ParentComment,
                m => m.MapFrom(s => s.ParentComment));
    }
}