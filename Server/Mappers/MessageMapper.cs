using AutoMapper;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;

namespace Harmonify.Server.Mappers;

public class MessageMapper : Profile
{
    public MessageMapper()
    {
        CreateMap<Message, MessageDTO>()
            .ForMember(p => p.Id,
                m => m.MapFrom(s => s.Id))
            .ForMember(p => p.Content,
                m => m.MapFrom(s => s.Content))
            .ForMember(p => p.FromUserId,
                m => m.MapFrom(s => s.FromUserId))
            .ForMember(p => p.FromUser,
                m => m.MapFrom(s => s.FromUser))
            .ForMember(p => p.ToUserId,
                m => m.MapFrom(s => s.ToUserId))
            .ForMember(p => p.ToUser,
                m => m.MapFrom(s => s.ToUser))
            .ForMember(p => p.SentOn,
                m => m.MapFrom(s => s.SentOn));
    }
}