using AutoMapper;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;

namespace Harmonify.Server.Mappers;

public class CommentLikeMapper : Profile
{
    public CommentLikeMapper()
    {
        CreateMap<CommentLike, CommentLikeDTO>()
            .ForMember(p => p.Id,
                m => m.MapFrom(s => s.Id))
            .ForMember(p => p.CommentId,
                m => m.MapFrom(s => s.CommentId))
            .ForMember(p => p.Comment,
                m => m.MapFrom(s => s.Comment))
            .ForMember(p => p.UserId,
                m => m.MapFrom(s => s.UserId))
            .ForMember(p => p.User,
                m => m.MapFrom(s => s.User));
    }}