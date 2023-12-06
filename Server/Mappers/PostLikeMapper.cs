using AutoMapper;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;

namespace Harmonify.Server.Mappers;

public class PostLikeMapper : Profile
{
    public PostLikeMapper()
    {
        CreateMap<PostLike, PostLikeDTO>()
            .ForMember(p => p.Id,
                m => m.MapFrom(s => s.Id))
            .ForMember(p => p.UserId, m => m.MapFrom(p => p.UserId))
            .ForMember(p => p.User, m => m.MapFrom(p => p.User))
            .ForMember(p => p.PostId, m => m.MapFrom(p => p.PostId))
            .ForMember(p => p.Post, m => m.MapFrom(p => p.Post));
    }
}