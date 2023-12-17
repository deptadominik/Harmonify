using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using MediatR;

namespace Harmonify.Server.Queries.Message;

public class GetMyChatQuery : IRequest<ICollection<MessageDTO>>
{
    public string UserId { get; init; }
    
    public string OtherUserId { get; init; }
    

    public class Handler : IRequestHandler<GetMyChatQuery, ICollection<MessageDTO>>
    {
        private readonly MessageRepository _repository;
        private readonly IMapper _mapper;

        public Handler(MessageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<MessageDTO>> Handle(GetMyChatQuery request,
            CancellationToken cancellationToken)
        {
            var messages = await _repository
                .GetMessagesAsync(request.UserId, request.OtherUserId);

            return _mapper.Map<ICollection<MessageDTO>>(messages);
        }
    }
}